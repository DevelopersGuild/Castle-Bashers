// Copyright (c) 2014 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace Rewired.Editor {

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public abstract class CustomInspector_External : UnityEditor.Editor {

        protected CustomInspector_Internal internalEditor;

        override public void OnInspectorGUI() {
            internalEditor.OnInspectorGUI();
        }

        protected void Enabled() {
            internalEditor.OnEnable();
        }
    }
}
