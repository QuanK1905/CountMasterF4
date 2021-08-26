using UnityEngine;

public class BowmanAnim : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;
  

    void Start()
    {

        animator.speed = 1f;
        animator.GetComponent<Animator>();
        
    }

  
    public void IsFighting()
    {

        animator.SetBool("isFighting", true);
    }
   

  
}
