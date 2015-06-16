// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

#if MONOMAC
using MonoMac.OpenGL;
#elif DESKTOPGL
using OpenTK.Graphics.OpenGL;
#elif GLES
using OpenTK.Graphics.ES20;
#endif

namespace Microsoft.Xna.Framework.Graphics
{
    public abstract partial class Texture
    {
        internal int glTexture = -1;
        internal TextureTarget glTarget;
        internal TextureUnit glTextureUnit = TextureUnit.Texture0;
        internal PixelInternalFormat glInternalFormat;
        internal PixelFormat glFormat;
        internal PixelType glType;
        internal System.Collections.Generic.Dictionary<int, SamplerState> glLastSamplerStates = new System.Collections.Generic.Dictionary<int, SamplerState>();

        private void PlatformGraphicsDeviceResetting()
        {
            DeleteGLTexture();
            glLastSamplerStates.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                DeleteGLTexture();
                glLastSamplerStates.Clear();
            }

            base.Dispose(disposing);
        }

        private void DeleteGLTexture()
        {
            if (glTexture > 0)
            {
                int texture = glTexture;
                Threading.BlockOnUIThread(() =>
                {
                    GL.DeleteTextures(1, ref texture);
                    GraphicsExtensions.CheckGLError();
                });
            }
            glTexture = -1;
        }
    }
}

