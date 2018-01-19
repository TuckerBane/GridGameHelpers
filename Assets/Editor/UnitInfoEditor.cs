using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitInfo))]
public class UnitInfoEditor : Editor
{
    Vector2 scrolly;
    bool foldOpen = false;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UnitInfo me = (UnitInfo)target;
        foldOpen = EditorGUILayout.Foldout(foldOpen, "Units");
        if (foldOpen)
        {
            foreach (GameObject unit in me.mUnits)
            {
                //EditorGUI.ObjectField;
                
                if (unit != null)
                    //EditorGUILayout.LabelField(unit.name);
                    EditorGUILayout.ObjectField(unit.name, unit, typeof(GameObject), false);
                else
                    EditorGUILayout.LabelField("dead unit");
            }
        }
    }
}
