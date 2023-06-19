using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ready() {
        if (Anim == null)
            Anim = GetComponent<Animator>();

        Anim.SetBool("Ready", true);
    }
}
