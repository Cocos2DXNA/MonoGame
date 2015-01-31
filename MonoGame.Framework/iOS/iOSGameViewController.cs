// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Drawing;

using UIKit;
using Foundation;
using CoreGraphics;

namespace Microsoft.Xna.Framework
{
    class iOSGameViewController : UIViewController
    {
        iOSGamePlatform _platform;

        public iOSGameViewController(iOSGamePlatform platform)
        {
            if (platform == null)
                throw new ArgumentNullException("platform");
            _platform = platform;
            SupportedOrientations = DisplayOrientation.Default;

            NSArray obj = (NSArray)NSBundle.MainBundle.ObjectForInfoDictionary("UISupportedInterfaceOrientations");

            for(nuint idx = 0; idx < obj.Count; ++idx)
            {
                string value = obj.GetItem<NSString>(idx).ToString();

                switch(value)
                {
                // NOTE: in XNA, Orientation Left is a 90 degree rotation counterclockwise, while on iOS
                // it is a 90 degree rotation CLOCKWISE. They are BACKWARDS!
                case "UIInterfaceOrientationLandscapeLeft":
                    SupportedOrientations |= DisplayOrientation.LandscapeRight;
                    break;

                case "UIInterfaceOrientationLandscapeRight":
                    SupportedOrientations |= DisplayOrientation.LandscapeLeft;
                    break;

                case "UIInterfaceOrientationPortrait":
                    SupportedOrientations |= DisplayOrientation.Portrait;
                    break;

                case "UIInterfaceOrientationPortraitUpsideDown":
                    SupportedOrientations |= DisplayOrientation.PortraitDown;
                    break;
                }
            }
        }

        public event EventHandler<EventArgs> InterfaceOrientationChanged;

        public DisplayOrientation SupportedOrientations { get; set; }

        public override void LoadView()
        {
			CGRect frame;
            if (ParentViewController != null && ParentViewController.View != null)
            {
				frame = new CGRect(CGPoint.Empty, ParentViewController.View.Frame.Size);
            }
            else
            {
                UIScreen screen = UIScreen.MainScreen;

                // iOS 7 and older reverses width/height in landscape mode when reporting resolution,
                // iOS 8+ reports resolution correctly in all cases
                if (InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
                {
					frame = new CGRect(0, 0, (nfloat)Math.Max(screen.Bounds.Width, screen.Bounds.Height), (nfloat)Math.Min(screen.Bounds.Width, screen.Bounds.Height));
                }
                else
                {
					frame = new CGRect(0, 0, screen.Bounds.Width, screen.Bounds.Height);
                }
            }

            base.View = new iOSGameView(_platform, frame);
        }

        public new iOSGameView View
        {
            get { return (iOSGameView)base.View; }
        }

        #region Autorotation for iOS 5 or older
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            DisplayOrientation supportedOrientations = OrientationConverter.Normalize(SupportedOrientations);
            var toOrientation = OrientationConverter.ToDisplayOrientation(toInterfaceOrientation);
            return (toOrientation & supportedOrientations) == toOrientation;
        }
        #endregion

        #region Autorotation for iOS 6 or newer
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return OrientationConverter.ToUIInterfaceOrientationMask(this.SupportedOrientations);
        }

        public override bool ShouldAutorotate()
        {
            return SupportedOrientations.HasFlag(DisplayOrientation.LandscapeLeft) || SupportedOrientations.HasFlag(DisplayOrientation.LandscapeRight) || _platform.Game.Initialized;
        }
        #endregion

        public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
        {
            base.DidRotate(fromInterfaceOrientation);

            var handler = InterfaceOrientationChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #region Hide statusbar for iOS 7 or newer
        public override bool PrefersStatusBarHidden()
        {
            return _platform.Game.graphicsDeviceManager.IsFullScreen;
        }
        #endregion


        #region iOS 8 or newer

		public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
			CGSize oldSize = View.Bounds.Size;

            if (oldSize != toSize)
            {
                UIInterfaceOrientation prevOrientation = InterfaceOrientation;

                // In iOS 8+ DidRotate is no longer called after a rotation
                // But we need to notify iOSGamePlatform to update back buffer so we explicitly call it 

                // We do this within the animateAlongside action, which at the point of calling
                // will have the new InterfaceOrientation set
                coordinator.AnimateAlongsideTransition((context) =>
                    {
                        DidRotate(prevOrientation);
                    }, (context) => 
                    {
                    });

            }

            base.ViewWillTransitionToSize(toSize, coordinator);
        }

        #endregion
    }
}
