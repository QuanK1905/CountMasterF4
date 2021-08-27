
using UnityEngine;

public class RunnerReturn : MonoBehaviour
{
    private RunnerPool objectPool;
    private Runner runner;
    private void Start()
    {
        objectPool = FindObjectOfType<RunnerPool>();
        runner = GetComponent<Runner>();
    }

    private void OnDisable()
    {
        if (objectPool != null)
            objectPool.ReturnRunner(runner);
    }
}