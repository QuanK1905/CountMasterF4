using System.Collections;
using UnityEngine;
using TMPro;
using JetSystems;
public class RunnerFormation : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private TextMeshPro squadAmountText;
    [SerializeField] private Transform bubble;
    [Header(" Formation Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float radiusFactor;
    [Range(0f, 1f)]
    [SerializeField] private float angleFactor;

    [Header(" Settings ")]
    [SerializeField] private Runner runnerPrefab;
    public int runnerStartCount ;
    public bool resetPosition = false;
    bool sound = false; 
    float resetSpeed ;
    public void Start()
    {
        AddRunners(runnerStartCount-1);
        sound = true;
        resetSpeed = 0.07f;
    }


    public void HideText()
    {
        bubble.gameObject.SetActive(false);
    }
   
    public void ResetPos()
    {
        resetPosition = !resetPosition;
    }
   

  
    void Update()
    {
       
        squadAmountText.text = transform.childCount.ToString();
        if (transform.childCount <= 0)
        { 
            transform.parent.gameObject.SetActive(false);
            UIManager.setGameoverDelegate?.Invoke();
            FindObjectOfType<Boss>().Idle();
        }
       
    }
  
    
  
    public float GetSquadRadius()
    {
        return radiusFactor * Mathf.Sqrt(transform.childCount);
    }
    public float GetSquadRadiusActive()
    {
        int k = 0;
        foreach (Transform c in transform)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return radiusFactor * Mathf.Sqrt(k);
    }

    public void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Runner runnerInstance = Instantiate(runnerPrefab, transform);
            runnerInstance.GetComponent<Runner>().StartRunning();

        }
        if (sound)
            Audio_Manager.instance.play("Play");
    }

    public void DelRunners(int amount)
    {
        if (amount > transform.childCount) amount = transform.childCount;
        
        for (int i = 0; i < amount; i++)
        {
           
            Transform runnerDel = transform.GetChild(i);

            runnerDel.GetComponent<Runner>().Explode();
        }
    }
}
