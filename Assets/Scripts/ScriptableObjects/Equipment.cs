using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum equipmentType
{
    Weapon,
    Offhand,
    Armor
}

[CreateAssetMenu(fileName ="newEquipment", menuName = "New ScriptableObject/New Equipment", order = 0)]
[System.Serializable]
public class Equipment : ScriptableObject
{
    
    public string name;
    public GameObject equipmentPrefab;
    public Vector3 eqiuipmentOffset;
    public Vector3 equipmentRotation;
    [Header("Stats")]
    public equipmentType Type;

    public int modifer;


}
