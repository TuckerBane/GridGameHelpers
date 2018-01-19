using UnityEngine;
using System.Collections;
using UnityEditor;

static class Extensions
{
    public static void PushBack<T>(ref T[] array, T item)
    {
        T[] newArray = new T[array.Length + 1];
        array.CopyTo(newArray, 0);
        newArray[array.Length] = item;
        
        array = newArray;
    }

    // requires exact object match
    public static void Erase<T>(ref T[] array, T item)
    {
        T[] newArray = new T[array.Length - 1];
        int offset = 0;
        for(int i = 0; i < array.Length; ++i)
        {
            if(array[i].Equals(item) && offset == 0)
            {
                offset -= 1;
            }
            else if(i + offset < newArray.Length)
            {
                newArray[i + offset] = array[i];
            }
            else
            {
                Debug.LogError("Array erase didn't find anything to erase");
            }

        }
        array = newArray;
    }

    public static void Test()
    {
        int[] pies = new int[0];
        PushBack(ref pies, 7);
        PushBack(ref pies, 7);
        PushBack(ref pies, 7);
    }
}

public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.LabelField(position, label);
        //EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}