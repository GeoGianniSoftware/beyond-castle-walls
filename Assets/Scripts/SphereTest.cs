using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{
    MeshRenderer MR;
    public float speed;

    private void Start() {
        MR = GetComponent<MeshRenderer>();
    }

    public void Update() {
        MR.material.mainTextureOffset += new Vector2(speed, 0f) * Time.deltaTime;
    }
}
