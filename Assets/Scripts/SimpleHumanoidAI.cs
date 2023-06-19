using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Sitting,
    Wondering,
    Attacking
}
public enum AIType
{
    Simple,
    Complex
}

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleHumanoidAI : Entity
{
    [Header("Components")]
    
    public Animator Anim;


    [Header("A.I. Stats")]
    public AIType AItype = AIType.Simple;
    public AIState currentState;
    public NavMeshAgent NMA;
    public float speed = 2f;
    public float maxWanderRange;
    public float maxWanderTime = 15.5f;
    public Transform idleCenter;
    public float moveVelocity;
    public float idleTime;
    public float actionDelay;
    public float distanceToDestination;

    [Header("Combat Stats")]
    public Entity combatTarget;
    public float attackRange;
    public float attackRate;
    public float attackTime = 0;
    public int damage;

    [Header("Equipment")]
    public bool canEquipItems;
    public Equipment currentEquipWeapon;
    public Equipment currentEquipOffhand;
    public List<CharacterEquipmentSlot> equipmentSlots;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        NMA = GetComponent<NavMeshAgent>();
        SimpleAISetup();


    }
    Vector3 randomWander = Vector3.zero;
    // Update is called once per frame


    E_Furniture currentSeat = null;

    List<E_Furniture> furnitureList = new List<E_Furniture>();
    public Entity target = null;

    void Update()
    {


        SimpleAITick();
      
        RandomWander();
        
    }

    public void Interact(Entity e) {
        //TODO: Fill this
    }

    public void Move(Vector3 location) {
        if (NMA.isActiveAndEnabled) {
            NMA.SetDestination(location);
        }
    }

    public void AttackCombatTarget() {
        if (combatTarget == null) {
            actionDelay = 2f;
            return;
        }
            

        if (combatTarget != null && combatTarget.currentHealth <= 0) {
            combatTarget = null;
            actionDelay = 2f;
            return;
        }
            

        currentState = AIState.Attacking;
        SimpleAITick();
        CloseRange();
        if (CheckDistanceToTarget()) {
            TryAttack();
        }
    }

    public void CloseRange() {
        if (CheckDistanceToTarget()) {
            if (NMA.remainingDistance <= attackRange) {
                NMA.speed = 0;
                return;
            }

        }
        else {
            NMA.speed = speed;
        }
    }

    public bool CheckDistanceToTarget() {
        if (combatTarget != null) {
            if (Vector3.Distance(NMA.destination, combatTarget.transform.position) > attackRange)
                NMA.SetDestination(combatTarget.transform.position);

            if (NMA.remainingDistance <= attackRange) {

                return true;
            }



        }
        return false;
    }

    public void TryAttack() {
        Vector3 direction = combatTarget.transform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, .8f);

        if (attackTime <= 0 && combatTarget != null && combatTarget.currentHealth > 0) {
            

            Anim.SetFloat("attackVariance", Random.Range(0.01f, 1f));
            Anim.SetTrigger("attack");
            StopCoroutine(startDamageDelay());
            StartCoroutine(startDamageDelay());
            attackTime = (Anim.GetCurrentAnimatorStateInfo(0).length * .45f) + attackRate;
        }
    }

    public void DealDamage() {
        if(combatTarget != null)
            combatTarget.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
    }

    public void SimpleAITick() {
        if(Anim == null)
            Anim = GetComponent<Animator>();

        if (NMA.isActiveAndEnabled) {

            moveVelocity = NMA.velocity.magnitude;
        }
        else {
            moveVelocity = 0f;
        }
        Anim.SetFloat("moveVelocity", moveVelocity);
        idleTime -= Time.deltaTime;
        attackTime -= Time.deltaTime;
        actionDelay -= Time.deltaTime;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void RandomWander() {
        foreach (E_Furniture f in GameObject.FindObjectsOfType<E_Furniture>()) {
            if (!furnitureList.Contains(f)) {
                furnitureList.Add(f);
            }
        }

        if (idleTime <= 0 && !Anim.GetBool("sitting")) {
            NMA.speed = speed;
            randomWander = getWanderPos();
            if (Random.Range(0, 4) == 3) {

                currentSeat = getEmptyFurniture();
            }
            idleTime += Random.Range(maxWanderTime / 3, maxWanderTime);
        }



        if (currentSeat == null) {
            currentState = AIState.Wondering;
            Wander();
        }
        else {
            if (currentSeat.canUse(this)) {
                SitDown(currentSeat);
            }

        }
    }


    int furnitureLoop = 0;
    E_Furniture getEmptyFurniture(bool loop = true) {
        furnitureLoop++;

        if (!loop) {
            furnitureLoop = 0;
            return null;
        }

        if(furnitureLoop > 10) {
            return getEmptyFurniture(false);
        }

        if(furnitureList.Count > 0) {
            int rand = Random.Range(0, furnitureList.Count);
            if (furnitureList[rand].canUse(this))
                return furnitureList[rand];

        }

        return getEmptyFurniture();

    }

    void Wander() {


        if(NMA.isActiveAndEnabled)
            NMA.SetDestination(randomWander);
    }

    void SitDown(E_Furniture loc) {
        if (loc.canUse(this)) {
            loc.user = this;
            
        }
        else {
            return;
        }

        if (NMA.isActiveAndEnabled) {
            NMA.SetDestination(loc.transform.position);
            if(target == null) {
                target = loc;
            }
            
            distanceToDestination = Vector3.Distance(transform.position, target.transform.position);
            
        }
            

        Vector3 newPos = new Vector3(loc.sitPosition.position.x, loc.sitPosition.position.y, loc.sitPosition.position.z);


        if(target == loc && distanceToDestination > .1f) {
            
            NMA.enabled = true;
            
        }else if (target == loc && distanceToDestination <= .1f) {
            NMA.enabled = false;
            transform.position = Vector3.Lerp(transform.position, newPos, .01f);
            transform.rotation = Quaternion.Lerp(transform.rotation, loc.sitPosition.rotation, .01f);

            if (currentState != AIState.Sitting) {
                idleTime += Random.Range(5f, 10f);
                currentState = AIState.Sitting;
            }

            Anim.SetBool("sitting", true);
            
            if(idleTime < 0) {

                RefreshAI(loc.transform.position);
                return;
            }
        }
        else if(target != loc && distanceToDestination > .1f) {
            
            Anim.SetBool("sitting", false);
            NMA.enabled = true;
            
        }
    }

    void RefreshAI(Vector3 pos) {
        actionDelay = Random.Range(2f, 4f);
        currentSeat.clear();
        currentSeat = null;
        print("GET UP");
        NMA.enabled = true;
        NMA.Warp(pos);
        target = null;
        Anim.SetBool("sitting", false);
        NMA.destination = transform.position;
    }

    int breakLoop = 0;
    Vector3 getWanderPos(bool loop = true) {
        if (!loop) {
            breakLoop = 0;
            return transform.position;
        }
            


        breakLoop++;
        Vector3 temp = idleCenter.position + Random.insideUnitSphere * maxWanderRange;
        temp.y = transform.position.y;
        NavMeshHit hit = new NavMeshHit();
        if (NavMesh.SamplePosition(temp, out hit, 1f, NavMesh.AllAreas)) {
           
            return hit.position;
        }

        if(breakLoop > 10) {
            return getWanderPos(false);
        }

        return getWanderPos();


    }

    public void SimpleAISetup() {
        InitHealthbar();
        InitEquipment();
    }

    public void InitEquipment() {
        if (canEquipItems) {
            foreach (CharacterEquipmentSlot slot in GetComponentsInChildren<CharacterEquipmentSlot>()) {
                equipmentSlots.Add(slot);
                if(currentEquipWeapon != null && slot.slotType == currentEquipWeapon.Type) {
                    slot.EquipSlot(currentEquipWeapon);
                }
                if (currentEquipOffhand != null && slot.slotType == currentEquipOffhand.Type) {
                    slot.EquipSlot(currentEquipOffhand);
                }
            }

            
        }
    }


    IEnumerator startDamageDelay() {

        float delay = Anim.GetCurrentAnimatorStateInfo(0).length*.55f;

        yield return new WaitForSeconds(delay);
        DealDamage();
    }


    public override void Die() {
        this.NMA.enabled = false;
        Anim.SetTrigger("dead");
        this.enabled = false;
    }
}
