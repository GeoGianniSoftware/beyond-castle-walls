using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterEquipmentSlot : MonoBehaviour
{
    public equipmentType slotType;
    public Equipment_Data currentlyEquipped = null;

    public void EquipSlot(Equipment equipmentToEquip) {
        if (equipmentToEquip.Type != slotType) {
            return;
        }

        if (currentlyEquipped != null) {
            Destroy(currentlyEquipped.gameObject);
            currentlyEquipped = null;
        }

        if(equipmentToEquip == null) {
            return;
        }


        GameObject temp = Instantiate(equipmentToEquip.equipmentPrefab, transform.position, Quaternion.identity);
        currentlyEquipped = temp.GetComponent<Equipment_Data>();
        temp.transform.SetParent(this.transform, true);
        temp.transform.localPosition = equipmentToEquip.eqiuipmentOffset;
        temp.transform.localRotation = Quaternion.Euler(equipmentToEquip.equipmentRotation);
        currentlyEquipped.currentSlot = this;

    }
}
