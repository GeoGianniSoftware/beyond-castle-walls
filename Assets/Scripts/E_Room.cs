using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Room : Entity
{
    public bool purchased = false;
    public int price = 100;

    public GameObject roomObject;
    public int wallFacing;

    public Canvas roomUI;
    public Text roomText;
    public Button roomButton;
    public Button roomPurchaseButton;

    public GameObject selectionQuad;



    PlayerController PC;
    // Start is called before the first frame update
    void Start()
    {
        PC = FindObjectOfType<PlayerController>();

        roomUI = GetComponentInChildren<Canvas>();
        roomText = roomUI.GetComponentInChildren<Text>();

        roomButton = roomUI.transform.GetChild(2).GetComponent<Button>();
        roomButton.gameObject.SetActive(false);

        roomPurchaseButton = roomUI.transform.GetChild(1).GetComponent<Button>();
        roomText.text = "$" + price;

        Material temp = new Material(selectionQuad.GetComponent<MeshRenderer>().material);
        temp.color = Color.white;

        selectionQuad.GetComponent<MeshRenderer>().material = temp;
    }

    public void SpawnRoom(string roomToSpawn) {
        Vector3 spawnPos = transform.position;
        spawnPos.y = -1.46f;

        Quaternion spawnRotation = Quaternion.identity;
        if(wallFacing == 1)
            spawnRotation = Quaternion.Euler(new Vector3(0, 180, 0));

        roomObject = Instantiate(Resources.Load("Rooms/" + roomToSpawn) as GameObject, spawnPos, spawnRotation);
        roomObject.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRoomDisplay() {
        roomText.text = NAME;
    }

    public void OnPurchaseRoomButton() {
        print("DING");
        NAME = "Empty";
        roomButton.gameObject.SetActive(true);
        roomPurchaseButton.gameObject.SetActive(false);
        purchased = true;
        UpdateRoomDisplay();
        FindObjectOfType<SelectionManager>().SelectObject(this);
    }

    public void OnRoomPanelButton() {
        PC.ShowRoomPanelUI(this);
    }

    public override bool CanSelect() {
        if (!purchased)
            return false;
        return true;
    }

    public override void Select() {
        if (!purchased) {
            return;
        }
           

        selected = true;
        Material temp = new Material(selectionQuad.GetComponent<MeshRenderer>().material);
        temp.color = Color.blue;

        selectionQuad.GetComponent<MeshRenderer>().material = temp;
        //GetComponent<MeshRenderer>().enabled = true;
    }

    

    public override void Deselect() {
        selected = false;
        Material temp = new Material(selectionQuad.GetComponent<MeshRenderer>().material);
        temp.color = Color.white;

        selectionQuad.GetComponent<MeshRenderer>().material = temp;
        //GetComponent<MeshRenderer>().enabled = false;
    }
}
