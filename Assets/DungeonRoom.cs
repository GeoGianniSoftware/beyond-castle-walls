using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DungeonRoom : MonoBehaviour
{
    public List<EnemyAI> enemies;
    public BoxCollider bounds;
    public List<Door> doors;

    public float roomPointsRemaining = 0;
    public bool roomClear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DungeonInit();
        DungeonRoomLogic();


    }

    public void DungeonInit() {
        if (enemies == null || enemies.Count == 0) {
            enemies = new List<EnemyAI>();
            fillRoomEnemies();
        }
    }

    public void DungeonRoomLogic() {
        CalculateRoomPoints();

        
    }

    public void CalculateRoomPoints() {
        if (enemies.Count > 0) {
            roomPointsRemaining = 0;
            foreach(EnemyAI e in enemies) {
                if(e.currentHealth > 0)
                    roomPointsRemaining += e.currentHealth;
            }
        }

        if (roomPointsRemaining == 0 && !roomClear) {
            roomClear = true;
            foreach(Door d in doors) {
                d.open = true;
            }
        }
    }

    public void fillRoomEnemies() {
        EnemyAI[] enemyList = GetComponentsInChildren<EnemyAI>();

        foreach(EnemyAI e in enemyList) {
            if(!enemies.Contains(e))
                enemies.Add(e);
        }
    }



    public Vector3 getRandomPointInBounds() {
        if (bounds == null)
            bounds = GetComponent<BoxCollider>();

        float maxX = bounds.size.x/2;
        float maxZ = bounds.size.z/2;

        float randX = Random.Range(-maxX, maxX);
        float randZ = Random.Range(-maxZ, maxZ);

        Vector3 pos = new Vector3(randX, transform.position.y, randZ);

        return transform.position + pos;
    }

}
