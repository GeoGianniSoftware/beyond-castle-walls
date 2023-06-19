using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxClouds : MonoBehaviour
{
    public int fxCount;
    public Vector2 fxAmount;
    public AnimationCurve powerCurve;

    // Start is called before the first frame update
    void Start()
    {
        fxCount = transform.childCount;
        for (int i = 0; i < fxCount; i++) {
            MRs.Add(transform.GetChild(0).GetComponent<MeshRenderer>());
        }
    }

    List<MeshRenderer> MRs = new List<MeshRenderer>();
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < MRs.Count; i++) {
            MRs[i].material.mainTextureOffset += ((fxAmount * Time.deltaTime) * ((i + 1) / fxCount))*powerCurve.Evaluate(((float)((i + 1) / fxCount)));
        }
        
    }
}
