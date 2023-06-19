using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SelectionManager SM;

    public GameObject UI_Room_Panel;

    // Start is called before the first frame update
    void Start()
    {
        SM = FindObjectOfType<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void ShowRoomPanelUI(E_Room roomToShow) {
        
        UI_Room_Panel.GetComponent<RoomPanel>().PopulateRoomPanel(roomToShow);
    }

    public void HideRoomPanelUI() {
        UI_Room_Panel.SetActive(false);
    }




}
