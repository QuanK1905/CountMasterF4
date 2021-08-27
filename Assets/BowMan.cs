using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMan : MonoBehaviour
{
    [Header(" Animation ")]
    [SerializeField] private Animator animator;
    void Start()
    {
        
    }
    public void IsFighting()
    {
        animator.SetInteger("State", 1);
        animator.SetBool("isFighting", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
