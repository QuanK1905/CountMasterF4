
using UnityEngine;

public class BowManGroup : MonoBehaviour
{
    [Header(" Detection ")]
    [SerializeField] private LayerMask runnersLayer;
    [SerializeField] private float detectionDistance;
    [SerializeField] private Transform bowmanParent;
    


    Vector3 pos;


    void Start()
    {
        pos = transform.position;
        pos.y = -2f;
    }

    bool findRunner = false;
    void Update()
    {
       
            FindTargetRunner();


        if (findRunner)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, 1 * Time.deltaTime);
            Handheld.Vibrate();
        }
        if (transform.position == pos) 
        { 
            Destroy(transform.gameObject);
            FindObjectOfType<SquadDetection>().FrezePos();
        }
    }

    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, detectionDistance, runnersLayer);

        if (detectedRunners.Length <= 0) return;


        for (int i = 0; i < bowmanParent.childCount; i++)
           bowmanParent.GetChild(i).GetComponent<BowmanAnim>().IsFighting();

        if (Vector3.Distance(transform.position, detectedRunners[0].transform.position) <= 2) findRunner =true;

    }

 

}
