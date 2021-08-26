using UnityEngine.AI;
using UnityEngine;
using JetSystems;

public class Runner : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private Renderer renderer;


    [Header(" Target Settings ")]
    private bool targeted;

    [Header(" Detection ")]
    [SerializeField] private LayerMask obstaclesLayer;
    private RunnerPooling objectPool;
    public Transform home;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<RunnerPooling>();
        home = transform.parent;
        animator.speed = Random.Range(1f, 2f);
        animator.GetComponent<Animator>();
    }
    private void OnDisable()
    {
        if (objectPool != null)
            objectPool.ReturnRunner(this.gameObject);
    }
    bool resetPos = true;
    void Update()
    {
       
        if (!collider.enabled)
            return;

        agent = this.GetComponent<NavMeshAgent>();
        if (home != null && resetPos)
        {
            float radius = FindObjectOfType<RunnerFormation>().GetSquadRadius();
            float distance = Vector3.Distance(transform.position, home.position);
            if (distance > radius) agent.speed = distance;
            else agent.speed = 0.1f;
        
        agent.SetDestination(home.position);
        }
        DetectObstacles();
        
    }
    public void ResetPos()
    {
        resetPos = !resetPos;
    }


    private void DetectObstacles()
    {
        if (Physics.OverlapSphere(transform.position, 0.1f, obstaclesLayer).Length > 0)
         
             Explode();
            
    }
    
    public void DetectCannon()
    {

        if (!gameObject.activeSelf) return;

            Vector3 pos = transform.parent.parent.position + 1*Vector3.forward;
            pos.x = 0;
        transform.position = Vector3.MoveTowards(transform.position, pos, 4 * Time.deltaTime);
        if (transform.position == pos) gameObject.SetActive(false);
       

    }
    public void IsFighting()
    {
      
      //  animator.SetBool("isRunning", false);
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
        Destroy(gameObject);
        Audio_Manager.instance.play("Runner_Die");
    }
}
 