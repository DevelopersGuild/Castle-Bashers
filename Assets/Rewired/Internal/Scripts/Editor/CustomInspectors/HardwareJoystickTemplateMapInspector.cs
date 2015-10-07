// Copyright (c) 2014 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using UnityEditor;
using Rewired.Data.Mapping;

namespace Rewired.Editor {

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    [CustomEditor(typeof(HardwareJoystickTemplateMap))]
    public sealed class HardwareJoystickTemplateMapInspector : CustomInspector_External {

        private void OnEnable() {
            internalEditor = new HardwareJoystickTemplateMapInspector_Internal(this);
            internalEditor.OnEnable();
        }
    }
}
