// Copyright (c) 2014 Augie R. Maddox, Guavaman Enterprises. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace Rewired.Editor {

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    [CustomEditor(typeof(Rewired.Data.ControllerDataFiles))]
    public sealed class ControllerDataFilesInspector : CustomInspector_External {

        private void OnEnable() {
            internalEditor = new ControllerDataFilesInspector_Internal(this);
            base.Enabled();
        }
    }
}
