using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class na : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position =transform.parent.position + 3*Vector3.forward;
    }
}
