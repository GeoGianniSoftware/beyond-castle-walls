using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterPanel : MonoBehaviour
{
    SimpleHumanoidAI selectedCharacter;
    PlayerController PC;
    SelectionManager SM;

    [Header("Character Panel")]
    public GameObject characterPanel;
    public Text nameText;
    public Text levelText;
    public Text tagText;
    public Text activityText;
    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();
        SM = FindObjectOfType<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(SM.SelectedObjects.Count > 0) {
            foreach(Entity e in SM.SelectedObjects) {
                if(e.TYPE == EntityType.UNIT) {
                    if (e.GetComponent<CharacterAI>() != null)
                        selectedCharacter = e.GetComponent<CharacterAI>();
                    else if (e.GetComponent<SimpleHumanoidAI>() != null)
                        selectedCharacter = e.GetComponent<SimpleHumanoidAI>();
                    PopulateCharacterPanel();
                }
            }
            
        }
        else {
            selectedCharacter = null;
        }

        if (selectedCharacter != null) {
            UpdateCharacterPanel();
        }
        characterPanel.SetActive(selectedCharacter != null);
    }

    public void PopulateCharacterPanel() {
        nameText.text = selectedCharacter.NAME;
        levelText.text = ""+selectedCharacter.LEVEL;
        if(selectedCharacter.AItype == AIType.Complex) {

            tagText.text = selectedCharacter.GetComponent<CharacterAI>().TAG;
        }
        activityText.text = selectedCharacter.currentState.ToString();
    }

    public void UpdateCharacterPanel() {
        if(levelText.text != "" + selectedCharacter.LEVEL)
            levelText.text = "" + selectedCharacter.LEVEL;
        if (activityText.text != selectedCharacter.currentState.ToString())
            activityText.text = selectedCharacter.currentState.ToString();
    }
}
