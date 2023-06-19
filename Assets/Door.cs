using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteInEditMode]
public class Door : MonoBehaviour
{
    public bool open;
    public bool invert;
    public GameObject[] doors = new GameObject[2];
    public Animator doorLock;

    NavMeshObstacle obstacle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Logic();
    }

    public void Logic() {
        if(obstacle == null)
            obstacle = GetComponent<NavMeshObstacle>();

        if (doors[0] == null)
            return;

        Quaternion beforeRot = doors[0].transform.rotation;
        float alt = 1;
        if (invert)
            alt = -1;
        if (open) {

            if (doorLock && !doorLock.GetBool("Opened")) {
                doorLock.SetBool("Ready", false);
                doorLock.SetBool("Opened", true);
            }

            if (doorLock == null || (doorLock && doorLock.GetBool("Ready")) || (doorLock && Application.isEditor && !Application.isPlaying)) {

                    
                doors[0].transform.localRotation = Quaternion.Euler(beforeRot.x, -90 * alt, beforeRot.z);
                doors[1].transform.localRotation = Quaternion.Euler(beforeRot.x, 90 * alt, beforeRot.z);
            }
            
            
        }
        else {

            

            if (doorLock == null || (doorLock && doorLock.GetBool("Ready")) || (doorLock && Application.isEditor && !Application.isPlaying)) {

                
                doors[0].transform.localRotation = Quaternion.Euler(beforeRot.x, 0, beforeRot.z);
                doors[1].transform.localRotation = Quaternion.Euler(beforeRot.x, 0, beforeRot.z);
            }

            if (doorLock && doorLock.GetBool("Opened")) {
                doorLock.SetBool("Opened", false);
            }

        }

        if(obstacle != null) {
            obstacle.enabled = !open;
        }
    }

    public void ToggleDoor() {
        
        open = !open;
        
            
    }
    
}
