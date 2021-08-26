
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    private RunnerPooling objectPool;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<RunnerPooling>();
    }

    // Update is called once per frame
    void Update()
    {
     //   timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeToSpawn)
        {
            GameObject newRunner = objectPool.GetRunner();
            newRunner.transform.position = this.transform.position;
            timeSinceSpawn = 0f;
        }
    }
}