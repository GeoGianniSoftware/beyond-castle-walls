using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntityType
{
    ROOM,
    UNIT
}

public class Entity : MonoBehaviour
{
    public string NAME;
    public EntityType TYPE;
    public int LEVEL;

    E_Healthbar healthBar;
    public int maxHealth;
    public int currentHealth;

    public bool isEnemy = false;

    public bool selected = false;

    public virtual void Select() {
        selected = true;
    }
    public virtual void Deselect() {
        selected = false;
    }

    public virtual bool CanSelect() {
        return true;
    }

    public void InitHealthbar() {
        GameObject hbarPrefab = Resources.Load("EntityHealthbar") as GameObject;
        GameObject hbarObj = Instantiate(hbarPrefab, transform.position, Quaternion.identity);
        healthBar = hbarObj.GetComponent<E_Healthbar>();
        healthBar.setTarget(this);
    }

    public void TakeDamage(int dmg) {
        currentHealth -= dmg;
        if(healthBar != null)
            healthBar.setHealthbarPercentage((float)currentHealth / (float)maxHealth);

        if(currentHealth <= 0) {
            Die();
        }

    }

    public virtual void Die() {
        Destroy(this.gameObject);
    }
}
