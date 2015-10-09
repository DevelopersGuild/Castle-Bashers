// Copyright (c) 2014 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using System.Collections.Generic;
using Rewired.Utils;
using Rewired.Platforms;

#pragma warning disable 0219

namespace Rewired {

    public sealed class InputManager : InputManager_Base {

        protected override void DetectPlatform() {
            // Set the editor and platform versions

            editorPlatform = EditorPlatform.None;
            platform = Platform.Unknown;
            webplayerPlatform = WebplayerPlatform.None;
            isEditor = false;
            string deviceName = SystemInfo.deviceName ?? string.Empty;
            string deviceModel = SystemInfo.deviceModel ?? string.Empty;

#if UNITY_EDITOR
            isEditor = true;
            editorPlatform = EditorPlatform.Unknown;
#endif

#if UNITY_EDITOR_WIN
            editorPlatform = EditorPlatform.Windows;
#endif

#if UNITY_EDITOR_OSX
            editorPlatform = EditorPlatform.OSX;
#endif

#if UNITY_STANDALONE_OSX
            platform = Platform.OSX;
            
#endif

#if UNITY_DASHBOARD_WIDGET

#endif

#if UNITY_STANDALONE_WIN
            platform = Platform.Windows;
#endif

#if UNITY_STANDALONE_LINUX
            platform = Platform.Linux;
#endif

#if UNITY_STANDALONE

#endif
            
#if UNITY_ANDROID
            platform = Platform.Android;
#if !UNITY_EDITOR
            // Handle Ouya platform
            if(deviceName.Contains("OUYA") || deviceModel.Contains("OUYA")) {
                platform = Platform.Ouya;
            }
#endif
#endif

#if UNITY_BLACKBERRY
            platform = Platform.Blackberry;
#endif

#if UNITY_WP8
            platform = Platform.WindowsPhone8;
#endif

#if UNITY_IPHONE
            platform = Platform.iOS;
#endif

#if UNITY_IOS
            platform = Platform.iOS;
#endif

#if UNITY_PS3
            platform = Platform.PS3;
#endif

#if UNITY_PS4
            platform = Platform.PS4;
#endif

#if UNITY_PSP2
            platform = Platform.PSVita;
#endif

#if UNITY_PSM
            platform = Platform.PSMobile;
#endif

#if UNITY_XBOX360
            platform = Platform.Xbox360;
#endif

#if UNITY_XBOXONE
            platform = Platform.XboxOne;
#endif

#if UNITY_WII
            platform = Platform.Wii;
#endif

#if UNITY_WIIU
            platform = Platform.WiiU;
#endif

#if UNITY_FLASH
            platform = Platform.Flash;
#endif

#if UNITY_METRO
            platform = Platform.WindowsAppStore;
#endif

#if UNITY_WINRT

#endif

#if UNITY_WEBGL
            platform = Platform.WebGL;
#endif

            // Check if Webplayer
#if UNITY_WEBPLAYER

            webplayerPlatform = UnityTools.DetermineWebplayerPlatformType(platform, editorPlatform); // call this BEFORE you change the platform so we still know what base system this is
            platform = Platform.Webplayer;

#endif
        }

        // Initialize the Platform Manager
        //protected override object InitializePlatform(TextAsset[] libraries, bool useXInput, UpdateLoopSetting updateLoopSetting) {
        //    return InputManagers.PlatformManager.Initialize(libraries, useXInput, (int)updateLoopSetting);
        //}

        protected override void CheckRecompile() {
#if UNITY_EDITOR
            // Destroy system if recompiling
            if(UnityEditor.EditorApplication.isCompiling) { // editor is recompiling
                if(!isCompiling) { // this is the first cycle of recompile
                    isCompiling = true; // flag it
                    RecompileStart();
                }
                return;
            }

            // Check for end of compile
            if(isCompiling) { // compiling is done
                isCompiling = false; // flag off
                RecompileEnd();
            }
#endif
        }


        protected override string GetFocusedEditorWindowTitle() {
#if UNITY_EDITOR
            UnityEditor.EditorWindow window = UnityEditor.EditorWindow.focusedWindow;
            return window != null ? window.title : string.Empty;
#else
            return string.Empty;
#endif
        }
    }
}
