using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_Healthbar : MonoBehaviour
{
    public Image healthBar;
    public Entity target;

    bool init = false;

    public void setHealthbarPercentage(float percentage) {
        healthBar.fillAmount = percentage;
    }

    public void setTarget(Entity e) {
        target = e;
        init = true;
    }

    private void Update() {
        if(target != null) {
            transform.position = target.transform.position;
        }

        if(init && target == null) {
            Destroy(this.gameObject);
        }
    }
}
