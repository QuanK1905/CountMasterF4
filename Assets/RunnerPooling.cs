
using System.Collections.Generic;
using UnityEngine;

public class RunnerPooling : MonoBehaviour
{
    [SerializeField] private GameObject runnerPrefab;
    public Transform RunnerParent;

    [SerializeField]
    private Queue<GameObject> runnerPool = new Queue<GameObject>();
    
   

    private void Start()
    {
      
    }

    public GameObject GetRunner()
    {
        if (runnerPool.Count > 0)
        {
            GameObject runner = runnerPool.Dequeue();
            runner.SetActive(true);
            runner.GetComponent<Runner>().StartRunning() ;
            return runner;
        }
        else
        {
            GameObject runner = Instantiate(runnerPrefab,RunnerParent);
            return runner;
        }
    }

    public void ReturnRunner(GameObject runner)
    {
        runnerPool.Enqueue(runner);
       runner.SetActive(false);
    }
}