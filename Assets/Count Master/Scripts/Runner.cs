using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;

    [Header(" Target Settings ")]
    private bool targeted;

    [Header(" Detection ")]
    [SerializeField] private LayerMask obstaclesLayer;
    [SerializeField] private LayerMask holeLayer;


    public NavMeshAgent agent;
    // Start is called before the first frame update

    Vector3 pos;
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
        
       
        if (home != null)
        { 
            agent.SetDestination(home.position);
        }
        DetectHole();
        DetectObstacles();

    }
    private void DetectObstacles()
    {
        if (Physics.OverlapSphere(transform.position, 0.1f, obstaclesLayer).Length > 0)

            Explode();
     }


    private void DetectHole()
    {     Collider[] detectHole = Physics.OverlapSphere(transform.position, 0.1f, holeLayer);
           if (detectHole.Length <= 0) return;

        Collider colliderHole = detectHole[0];
    {

      //      transform.LookAt(colliderHole.transform);
            IsFalling();
            transform.parent = null;
            StartCoroutine(Die());
            IEnumerator Die()
            {
                yield return new WaitForSeconds(0.5f);
                Explode();
            }
        }
      

    }
    
   
    public void IsFighting()
    {
        animator.SetBool("isFighting",true);
    }
    public void IsFalling()
    {
        animator.SetBool("isFalling", true);
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
 