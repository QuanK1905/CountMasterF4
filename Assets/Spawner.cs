
using UnityEngine;

public class Spawner : MonoBehaviour
{
  
 
    private RunnerPool objectPool;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<RunnerPool>();
       // AddRunner(340);
    }

    public void AddRunner(int amount)
    {
      for(int i=0;i<amount;i++)
        {
            Runner newRunner = objectPool.GetRunner();
            newRunner.transform.position = this.transform.position + 0.1f*Vector3.back;
           
        }
    }
  
}