using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : SimpleHumanoidAI
{
    [Header("Randomize")]
    public bool randomizeApperance = false;
    public bool randomizeSize = false;

    public float minSize, maxSize;
    public List<Material> materialVariants = new List<Material>();
    public List<GameObject> skinVariants = new List<GameObject>();
    
    [Header("Equipment Pool")]
    public List<Equipment> weaponList = new List<Equipment>();
    public List<Equipment> offhandList = new List<Equipment>();

    void Start() {
        Anim = GetComponent<Animator>();
        NMA = GetComponent<NavMeshAgent>();


        RandomizeEnemy();
        SimpleAISetup();
        
    }

    void RandomizeEnemy() {
        if(randomizeSize)
        transform.localScale *= Random.Range(minSize, maxSize);

        if (randomizeApperance) {
            if (skinVariants.Count > 1) {
                int rand = Random.Range(0, skinVariants.Count);

                if (rand != 0) {
                    skinVariants[0].SetActive(false);
                    skinVariants[rand].SetActive(true);
                }
            }

            if (materialVariants.Count > 1) {
                int rand = Random.Range(0, materialVariants.Count);
                int rare = Random.Range(0, 500);


                GetComponentInChildren<SkinnedMeshRenderer>().material = materialVariants[rand];

                if (rare == 1) {
                    print("Rainbow Enemy");
                    Material temp = new Material(GetComponentInChildren<SkinnedMeshRenderer>().material);
                    Color c = Random.ColorHSV();
                    if ((c.r + c.g + c.b) < .25f) {
                        c = Random.ColorHSV();
                    }
                    temp.color = c;
                    GetComponentInChildren<SkinnedMeshRenderer>().material = temp;
                }


            }
        }
        

        if (weaponList.Count > 1) {
            currentEquipWeapon = weaponList[Random.Range(0, weaponList.Count)];
        }
        if (offhandList.Count > 1) {
            currentEquipOffhand = offhandList[Random.Range(0, offhandList.Count)];
        }
    }

    private void Update() {

        SimpleAITick();

        SearchAndEngageTargets();
        if (combatTarget == null) {
            RandomWander();
        }

        

    }


    void SearchAndEngageTargets() {
        if (combatTarget == null) {
            Collider[] cols = Physics.OverlapSphere(transform.position, attackRange * 5f);
            foreach (Collider c in cols) {
                CharacterAI enemy = c.GetComponent<CharacterAI>();
                if (enemy != null && enemy.currentHealth > 0) {
                    combatTarget = c.GetComponent<CharacterAI>();
                }
            }
        }
        else {
            AttackCombatTarget();
        }
    }

}
