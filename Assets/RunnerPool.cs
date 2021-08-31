using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPool : MonoBehaviour
{
    [SerializeField]
    private Runner runnerPrefab;
    [SerializeField]
    private Transform runnerParent;
    [SerializeField]
    private Queue<Runner> runnerPool = new Queue<Runner>();
    [SerializeField]
    private int poolStartSize = 5;

    private void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
           Runner runner = Instantiate(runnerPrefab, runnerParent);
            runnerPool.Enqueue(runner);
            runner.gameObject.SetActive(false);
        }
    }

    public Runner GetRunner()
    {
        if (runnerPool.Count > 0)
        {
            Runner runner = runnerPool.Dequeue();
            
            runner.gameObject.SetActive(true);
            runner.StartRunning();
            return runner;
        }
        else
        {
            Runner runner = Instantiate(runnerPrefab,runnerParent);
            runner.StartRunning();
            return runner;
        }
    }
    public Runner GetStartRunner()
    {
        if (runnerPool.Count > 0)
        {
            Runner runner = runnerPool.Dequeue();

            runner.gameObject.SetActive(true);
            
            return runner;
        }
        else
        {
            Runner runner = Instantiate(runnerPrefab, runnerParent);
           
            return runner;
        }
    }

    public void ReturnRunner(Runner runner)
    {
        runnerPool.Enqueue(runner);
       runner.gameObject.SetActive(false);
    }
}