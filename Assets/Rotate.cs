using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
  
    void Update()
    {
         transform.Rotate(new Vector3(0,100*Time.deltaTime,0));
        
    }
}
