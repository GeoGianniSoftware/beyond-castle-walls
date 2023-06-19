using XNodeEditor;
using XNode;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI;

[CustomNodeEditor(typeof(Node))]
public class BehaviorNodeCustomEditor : NodeEditor
{
    BehaviorNode node;
    public override void OnBodyGUI() {
        if (node == null) node = target as BehaviorNode;

        // Update serialized object's representation
        serializedObject.Update();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("name"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("transitionFROM"));
        //UnityEditor.EditorGUILayout.LabelField("The value is " + simpleNode.GetSum());
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("transitionTO"));

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("testTexture"));


        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("canMove"));

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("speedMod"));
        // Apply property modifications
        GUILayout.Space(10);
        if (node.testTexture) {
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            Rect r = GUILayoutUtility.GetRect(120, 120);

            GUI.DrawTexture(r, node.testTexture);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
            


        serializedObject.ApplyModifiedProperties();
    }


}
