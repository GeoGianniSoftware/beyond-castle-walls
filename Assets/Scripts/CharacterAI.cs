using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : SimpleHumanoidAI
{
    public string TAG;
    private void Start() {
         AItype = AIType.Complex;
         SimpleAISetup();
    }

    private void Update() {
        

        SimpleAITick();

        if(actionDelay > 0) {
            NMA.speed = 0;
            return;
        }

        SearchAndEngageTargets();
       

        

        if(combatTarget == null) {

            RandomWander();
        }
            

    }

    void SearchAndEngageTargets() {
        if (combatTarget == null) {
            Collider[] cols = Physics.OverlapSphere(transform.position, attackRange * 5f);
            foreach (Collider c in cols) {
                EnemyAI enemy = c.GetComponent<EnemyAI>();
                if (enemy != null && enemy.currentHealth > 0) {
                    combatTarget = c.GetComponent<EnemyAI>();
                }
            }
        }
        else {
            AttackCombatTarget();
        }
    }

}
