using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIDebug
{
    class ButtonListVertical{
        Vector2 _scr;
        void Draw(string label,GUIStyle style){
            GUILayout.Label(label);
            _scr = GUILayout.BeginScrollView(_scr, style);
            GUILayout.BeginVertical();
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
    }
}
