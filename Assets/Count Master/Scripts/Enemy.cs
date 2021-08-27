using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header(" Detection ")]
    [SerializeField] private LayerMask runnersLayer;
    [SerializeField] private float detectionDistance;
    private Runner targetRunner;

    [Header(" Movement ")]
    [SerializeField] private float moveSpeed = 11f;

    [Header(" Animation ")]
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRunner == null)
            FindTargetRunner();
        else
            AttackRunner();
    }

    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, detectionDistance, runnersLayer);

        if (detectedRunners.Length <= 0) return;

        for (int i = 0; i < detectedRunners.Length; i++)
        {
            Runner currentRunner = detectedRunners[i].GetComponent<Runner>();
            if (currentRunner.IsTargeted()) continue;

            currentRunner.SetAsTarget();
            targetRunner = currentRunner;
            StartMoving();
            break;
        }


    }

    private void AttackRunner()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.transform.position, moveSpeed * Time.deltaTime);
        transform.forward = (targetRunner.transform.position - transform.position).normalized;

        if(Vector3.Distance(transform.position, targetRunner.transform.position) <= 1f)
        {
            targetRunner.Explode();
            Explode();
        }
    }

    private void StartMoving()
    {
        animator.SetInteger("State", 1);
        transform.parent = null;
    }


    private void Explode()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }

    
}
