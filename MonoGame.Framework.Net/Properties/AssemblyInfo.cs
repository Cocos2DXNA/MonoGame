using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Resources;
//
// This file was generated for the Cocos2D-XNA project. It is subject to the terms and license 
// governing the Cocos2D-XNA project. 
// https://github.com/Cocos2DXNA/cocos2d-xna/blob/master/LicenseAndCredit.txt
//
// (c) Cocos2D-XNA Group 
// (c) Totally Evil Entertainment, LLC
//
// 
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MonoGame.Framework.Net")]
#if OUYA
[assembly: AssemblyDescription("MonoGame Gamer Services for OUYA")]
#elif ANDROID
[assembly: AssemblyDescription("MonoGame Gamer Services for Android")]
#elif WINDOWS_STOREAPP
[assembly: AssemblyDescription("MonoGame Gamer Services for Windows Store")]
#elif WINDOWS
#if DIRECTX
[assembly: AssemblyDescription("MonoGame Gamer Services for Windows Desktop (DirectX)")]
#else
[assembly: AssemblyDescription("MonoGame Gamer Services for Windows Desktop (OpenGL)")]
#endif
#elif PSM
[assembly: AssemblyDescription("MonoGame Gamer Services for PlayStation Mobile")]
#elif LINUX
[assembly: AssemblyDescription("MonoGame Gamer Services for Linux")]
#elif MAC
[assembly: AssemblyDescription("MonoGame Gamer Services for Mac OS X")]
#elif IOS
[assembly: AssemblyDescription("MonoGame Gamer Services for iOS")]
#elif WINDOWS_PHONE
[assembly: AssemblyDescription("MonoGame Gamer Services for Windows Phone 8")]
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MonoGame.Framework.Net")]
[assembly: AssemblyCopyright("Copyright © 2011-2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Mark the assembly as CLS compliant so it can be safely used in other .NET languages
[assembly:CLSCompliant(true)]

// Allow the content pipeline assembly to access 
// some of our internal helper methods that it needs.
[assembly: InternalsVisibleTo("MonoGame.Framework.Content.Pipeline")]
[assembly: InternalsVisibleTo("MonoGame.Framework")]

//Tests projects need access too
[assembly: InternalsVisibleTo("MonoGameTests")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("F3B849CA-FE65-4A78-9AEE-56140C4C6EAD")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("3.1.2.0")]
[assembly: AssemblyFileVersion("3.1.2.0")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]