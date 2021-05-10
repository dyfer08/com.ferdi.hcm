using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System;

namespace Ferdi{
public class HCM_Window : EditorWindow{

    Vector2 ScrollPos;
    static HCM_Data DatabaseObject = null;
    SerializedObject SO;
    public ReorderableList ColorList = null;
    public static HCM_Window Instance { get;set; }

    [MenuItem("Window/Hierarchy Color Manager")]
    public static void ShowWindow(){
        EditorWindow.GetWindow<HCM_Window>("Hierarchy Color Manager");
    }
 
    void OnEnable(){
        
        DatabaseObject = (HCM_Data)AssetDatabase.LoadAssetAtPath("Packages/com.ferdi.hcm/Settings/HCM.asset", typeof(HCM_Data));

        Instance = this;
        SO = new SerializedObject(DatabaseObject);
        ColorList = new ReorderableList(SO, SO.FindProperty("TagColors"),true, false, true, true);
        ColorList.drawElementCallback = (Rect Rect, int Index, bool IsActive, bool IsFocused) =>{
            var Element = ColorList.serializedProperty.GetArrayElementAtIndex(Index);
            Rect.y += 2;
            Element.FindPropertyRelative("Tag").stringValue = EditorGUI.TagField(new Rect(Rect.x + 2, Rect.y, (EditorGUIUtility.currentViewWidth/2), EditorGUIUtility.singleLineHeight), Element.FindPropertyRelative("Tag").stringValue);
            EditorGUI.PropertyField( new Rect((EditorGUIUtility.currentViewWidth/2) + 30, Rect.y, EditorGUIUtility.currentViewWidth/2 - 40, EditorGUIUtility.singleLineHeight), Element.FindPropertyRelative("Color"), GUIContent.none);
        };

        ColorList.onAddCallback = (ReorderableList RList) => {
            var Index = RList.serializedProperty.arraySize;
            RList.serializedProperty.arraySize++;
            RList.index = Index;
            var Element = RList.serializedProperty.GetArrayElementAtIndex(Index);
            Element.FindPropertyRelative("Tag").stringValue = "Untagged";
            Element.FindPropertyRelative("Color").colorValue = Color.white;
        };
    }

 
    void OnGUI(){
        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos);
        
        GUILayout.Space(10);

        EditorGUI.DrawRect(new Rect(0, 0, Screen.width, 1), new Color32(0,0,0,100));
        EditorGUI.DrawRect(new Rect(0, 1, Screen.width, 32), new Color32(0,0,0,25));
        EditorGUI.DrawRect(new Rect(0, 32, Screen.width, 1), new Color32(0,0,0,100));

        EditorGUILayout.BeginHorizontal ();
            GUILayout.FlexibleSpace();
            GUILayout.Label ("Hierarchy Color Manager", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(5);

        SO.Update();
        ColorList.DoLayoutList();
        SO.ApplyModifiedProperties();
        GUILayout.Space(10);
        EditorGUILayout.EndScrollView();
    }
}
}