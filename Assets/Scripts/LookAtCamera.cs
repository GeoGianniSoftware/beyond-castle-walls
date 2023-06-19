using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var lookPos = Camera.main.transform.position - transform.position;
        lookPos.z = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        transform.rotation = Quaternion.Euler(90, 0, rotation.z);
    }
}
