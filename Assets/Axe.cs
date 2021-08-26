using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    void Start()
    {
      
        animator.speed = speed*Random.Range(1f, 1.5f);
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
