using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoteCam : MonoBehaviour
{
    public Transform runnerParent;
    

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetRunner = runnerParent.position + Vector3.forward;
        
           targetRunner.x = Mathf.Clamp(targetRunner.x, -2, 2);
       
        transform.position =  targetRunner;
    }
}
