
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Header(" Detection ")]
    [SerializeField] private LayerMask runnersLayer;
    [SerializeField] private float detectionDistance;
    [SerializeField] private Transform fightPoint;
    private Runner targetRunner;

    [Header(" Movement ")]
    [SerializeField] private float moveSpeed;

    [Header(" Animation ")]
    [SerializeField] private Animator animator;



    public int strength;
    public float health = 100;
    public float maxHealth = 100;
    
    public GameObject healthBarUI;
    public Slider slider;
    void Start()
    {
        animator.speed = 1.5f;
        slider.value = CalculateHealth();

    }
    bool foundRunner = false;
    bool stopFight = false;
    void Update()
    {
       
            
        slider.value = CalculateHealth();
        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        if(health <=0)
        {
            Explode();
            FindObjectOfType<SquadController>().RunStatus();

        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }

            FindTargetRunner();

        if (foundRunner)
        {
           
            transform.position = Vector3.MoveTowards(transform.position, fightPoint.position, 10 * Time.deltaTime);
        }
        if (transform.position == fightPoint.position)
        if(!stopFight){
            
            IsFighting();
            health -= 15*Time.deltaTime;
            
        }
    }
   
    public void IsFighting()
    {
        animator.SetBool("isFighting", true);
    }
    public void Idle()
    {
        animator.SetInteger("State", 0);
        animator.SetBool("isFighting", false);
        animator.SetBool("isRunning", true);
        stopFight = true;
    }
    float CalculateHealth()
    {
        return health / maxHealth;
    }
    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, detectionDistance, runnersLayer);
        
        if (detectedRunners.Length <= 0) return;
     
        StartMoving();
        foundRunner = true;
        
        
    }
    private void StartMoving()
    {
        animator.SetInteger("State", 1);
        transform.parent = null;
    }


    private void Explode()
    {
        Destroy(transform.gameObject);
       // FindObjectOfType<SquadDetection>().SetLevelComplete();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }


}
