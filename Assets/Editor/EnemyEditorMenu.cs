using UnityEditor;

[CustomEditor(typeof(EnemyAI), true)]
public class EnemyEditorMenu : AdvancedEntityEditorMenu
{
   


    // The function that makes the custom editor work
    public override void OnInspectorGUI() {
        // Display the enum popup in the inspector
        categoryToDisplay = (DisplayCategory)EditorGUILayout.EnumPopup("Display", categoryToDisplay);
        // Create a space to separate this enum popup from other variables 
        EditorGUILayout.Space();

        // Switch statement to handle what happens for each category
        switch (categoryToDisplay) {
            case DisplayCategory.Entity:
                DisplayEntityInfo();
                break;

            case DisplayCategory.AI:
                DisplayAIInfo();
                break;

            case DisplayCategory.Combat:
                DisplayCombatInfo();
                break;

            case DisplayCategory.Character:
                DisplayCharacterInfo();
                break;

            case (DisplayCategory)DisplayCategory.Enemy:
                DisplayEnemyInfo();
                break;

            case DisplayCategory.Equipment:
                DisplayEquipmentInfo();
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }


    void DisplayEnemyInfo() {
        // Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty isEnemy = serializedObject.FindProperty("isEnemy");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(isEnemy);
        if (isEnemy.boolValue) {




            // Store the hasMagic bool as a serializedProperty so we can access it
            SerializedProperty randomize = serializedObject.FindProperty("randomizeApperance");

            // Draw a property for the hasMagic bool
            EditorGUILayout.PropertyField(randomize);
            if (randomize.boolValue) {
                // Store the hasMagic bool as a serializedProperty so we can access it
                SerializedProperty randomizeS = serializedObject.FindProperty("randomizeSize");

                // Draw a property for the hasMagic bool
                EditorGUILayout.PropertyField(randomizeS);
                if (randomizeS.boolValue) {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("minSize"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSize"));
                }
                    
                EditorGUILayout.PropertyField(serializedObject.FindProperty("materialVariants"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("skinVariants"));
            }
                
        }
    }

    void DisplayEquipmentInfo() {
        base.DisplayEquipmentInfo();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("weaponList"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("offhandList"));
        
    }
}