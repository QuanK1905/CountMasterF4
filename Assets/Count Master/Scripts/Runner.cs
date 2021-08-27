using UnityEngine.AI;
using UnityEngine;


public class Runner : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;


    [Header(" Target Settings ")]
    private bool targeted;

    [Header(" Detection ")]
    [SerializeField] private LayerMask obstaclesLayer;
    
   
   
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       
        home = transform.parent;
        animator.speed = Random.Range(1f, 2f);
        animator.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }
    public Transform home;
  
    void Update()
    {
       
        

        if (home != null )
        {
     
         //   float radius = FindObjectOfType<RunnerFormation>().GetSquadRadius();
         //   float distance = Vector3.Distance(transform.position, home.position);
         //   if (distance > radius) agent.speed = distance;
         //   else agent.speed = 0.05f;
        
            agent.SetDestination(home.position);
        }
        DetectObstacles();
        
    }
    


    private void DetectObstacles()
    {
        if (Physics.OverlapSphere(transform.position, 0.1f, obstaclesLayer).Length > 0)
         
             Explode();
            
    }
    
   
    public void IsFighting()
    {
        animator.SetBool("isFighting",true);
    }
    public void StopFighting()
    {
        animator.SetBool("isFighting", false);
        animator.SetBool("isRunning", true);
    }
    public void IsDancing()
    {
        animator.SetInteger("State", 1);
        animator.SetBool("isDancing", true);
     
    }
  
   public void StartRunning()
    {
        animator.SetInteger("State", 1);

    }
    public void StopRunning()
    {
        animator.SetInteger("State", 0);
        
    }

    public void SetAsTarget()
    {
        targeted = true;
    }

    public bool IsTargeted()
    {
        return targeted;
    }

   
    public void Explode()
    {
        gameObject.SetActive(false);
        Audio_Manager.instance.play("Runner_Die");
    }
}
 