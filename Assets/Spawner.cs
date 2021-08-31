
using UnityEngine;

public class Spawner : MonoBehaviour
{
  
 
    private RunnerPool objectPool;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<RunnerPool>();
        int runnerStart = PlayerPrefs.GetInt("UNITLEVEL");
        AddRunner(runnerStart-1);
    }

    public void AddRunner(int amount)
    {
        if (amount == 0) return;
      for(int i=0;i<amount;i++)
        {
            Runner newRunner = objectPool.GetRunner();
            newRunner.transform.position = this.transform.position + 0.1f*Vector3.left;
           
        }
    }
    public void AddStartRunner(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Runner newRunner = objectPool.GetStartRunner();
            newRunner.transform.position = this.transform.position + 0.1f * Vector3.left;

        }
    }

}