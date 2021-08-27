using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform RotateCam;
    public float rotateSpeed;
    bool fightCannon = false;
    bool fightBoss = false;
    bool end = false;
    private void Start()
    {
    }

    public void ChangeView()
    {
        fightBoss = true;
        
    }
    public void ChangeViewCannon()
    {
        fightCannon = true;

    }
    public void ChangeViewE()
    {
        end = true;

    }
    void LateUpdate()
    { 
     
        if(fightBoss)
           if( RotateCam.rotation.y >= -0.45 )
             {    
                RotateCam.Rotate(0, rotateSpeed * Time.deltaTime, 0);
             }
       if(end )
            if(Vector3.Distance(transform.position,player.position) > 17f)
               transform.position = Vector3.MoveTowards(transform.position, player.position, 8 * Time.deltaTime);
        if (fightCannon)
        {
            transform.parent = null;
            if (Vector3.Distance(transform.position, player.position) > 18f)
                transform.position = Vector3.MoveTowards(transform.position, player.position+ new Vector3(0,50,300), 8 * Time.deltaTime);
        }

    }
}
