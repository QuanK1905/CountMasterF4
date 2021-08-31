using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [Header(" Effects ")]
    [SerializeField] private ParticleSystem explodeParticles;
    void Start()
    {
        
    }
    int count = 0;
    public void NewEvent()
    {
        count++;
        explodeParticles.Play();
        Audio_Manager.instance.play("Bomb");
        FindObjectOfType<RunnerFormation>().DelRunner(5);
       
    }
    // Update is called once per frame
 
}
