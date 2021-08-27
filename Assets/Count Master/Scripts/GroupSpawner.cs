using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int amount;
    [SerializeField] private Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
            Instantiate(objectToSpawn, parent);        
    }

    private void Update()
    {
        if (parent.childCount <= 0)
            Destroy(gameObject);
    }
}
