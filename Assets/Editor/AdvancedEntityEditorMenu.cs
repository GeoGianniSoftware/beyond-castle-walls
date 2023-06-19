using UnityEditor;

[CustomEditor(typeof(SimpleHumanoidAI), true)]
public class AdvancedEntityEditorMenu : Editor
{
    // The various categories the editor will display the variables in 
    public enum DisplayCategory
    {
        Entity, AI, Combat, Enemy, Character, Equipment
    }

    // The enum field that will determine what variables to display in the Inspector
    public DisplayCategory categoryToDisplay;

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

            case DisplayCategory.Equipment:
                DisplayEquipmentInfo();
                break;

        }
        serializedObject.ApplyModifiedProperties();
    }

    /*public float speed = 2f;
    public float maxWanderRange;
    public float maxWanderTime = 15.5f;
    public Transform idleCenter;
    public float moveVelocity;
    public float idleTime;

    public string NAME;
    public EntityType TYPE;

    public bool selected = false;
     * 
     */

    // When the categoryToDisplay enum is at "Basic"
    public void DisplayEntityInfo() {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("NAME"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("LEVEL"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("TYPE"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("selected"));
    }

    // When the categoryToDisplay enum is at "Combat"
    public void DisplayAIInfo() {
        // Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty hasAI = serializedObject.FindProperty("NMA");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(hasAI);
        if(hasAI.objectReferenceValue != null) {

            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentState"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxWanderRange"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxWanderTime"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("idleCenter"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("moveVelocity"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("idleTime"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("actionDelay"));
        }

    }

    // When the categoryToDisplay enum is at "Magic"
    public void DisplayCharacterInfo() {
        /*EditorGUILayout.PropertyField(serializedObject.FindProperty("magicResistance"));

        // Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty hasMagicProperty = serializedObject.FindProperty("hasMagic");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(hasMagicProperty);

        // Check if hasMagic is true
        if (hasMagicProperty.boolValue) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mana"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicDamage"));
        }
        */
    }

    /*
     * [Header("Combat Stats")]
    public Entity combatTarget;
    public float attackRange;
    public float attackRate;
    public float actionIdle = 0;
    
    public int MaxHealth;
    public int CurrentHealth;
    public int damage;
     */
    public void DisplayCombatInfo() {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentHealth"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("combatTarget"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRange"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackRate"));


        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackTime"));
    }


    public virtual void DisplayEquipmentInfo() {
        // Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty canEquip = serializedObject.FindProperty("canEquipItems");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(canEquip);
        if (canEquip.boolValue) {

            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentEquipWeapon"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("currentEquipOffhand"));
        }
    }
}