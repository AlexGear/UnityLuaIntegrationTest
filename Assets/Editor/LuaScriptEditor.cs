using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LuaScript))]
public class LuaScriptEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        LuaScript script = (LuaScript)target;

        if(!Application.isPlaying)
            return;
        
        if(string.IsNullOrEmpty(script.error)) {
            EditorGUILayout.HelpBox("OK", MessageType.Info);
        }
        else {
            EditorGUILayout.HelpBox(script.error, MessageType.Error);
        }
    }
}
