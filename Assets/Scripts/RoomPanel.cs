using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel: MonoBehaviour
{
    string[] temp = new string[] { "Armory", "Tavern", "Blue" };
    public E_Room connectedRoom;

    public Text roomNameText;

    public GameObject controlPanel;
    public List<GameObject> controlPanelButtons = new List<GameObject>();


    public void HidePanel() {
        this.gameObject.SetActive(false);
    }

    public void PopulateRoomPanel(E_Room roomToDisplay) {
        connectedRoom = roomToDisplay;
        roomNameText.text = connectedRoom.NAME;
        controlPanelButtons.Clear();
        for (int i = 0; i < controlPanel.transform.childCount; i++) {
            controlPanelButtons.Add(controlPanel.transform.GetChild(i).gameObject);
            controlPanel.transform.GetChild(i).gameObject.SetActive(true);
            controlPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = temp[i];
        }

        if (connectedRoom.roomObject != null) {
            foreach (GameObject g in controlPanelButtons) {
                g.SetActive(false);
            }
        }

        gameObject.SetActive(true);
        
    }

    public void OnControlPanelButton(int index) {
        connectedRoom.NAME = temp[index];
        connectedRoom.SpawnRoom(temp[index]);
        connectedRoom.UpdateRoomDisplay();
        PopulateRoomPanel(connectedRoom);
    }
}
