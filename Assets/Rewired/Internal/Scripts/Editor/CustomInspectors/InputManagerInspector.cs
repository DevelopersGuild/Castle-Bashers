// Copyright (c) 2014 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace Rewired.Editor {

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    [CustomEditor(typeof(InputManager))]
    public sealed class InputManagerInspector : CustomInspector_External {

        private void OnEnable() {
            internalEditor = new InputManagerInspector_Internal(this);
            internalEditor.OnEnable();
        }
    }
}