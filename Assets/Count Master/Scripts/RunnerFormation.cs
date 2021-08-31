
using UnityEngine;
using TMPro;
using JetSystems;
public class RunnerFormation : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private TextMeshPro squadAmountText;
    [SerializeField] private Transform bubble;


    public void HideText()
    {
        bubble.gameObject.SetActive(false);
    }
    int k;
    void Update()
    {
       
        k= 0;
        foreach (Transform c in transform)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        squadAmountText.text = k.ToString();
        if (k <= 0)
        { 
            transform.parent.gameObject.SetActive(false);
            UIManager.setGameoverDelegate?.Invoke();
            FindObjectOfType<Boss>().Idle();
        }
       
    }

   public void DelRunner(int amount)
    {
        if (amount > k) amount = k;
        int del = amount;
        foreach (Transform c in transform)

            if (c.gameObject.activeSelf)
            {
                c.gameObject.SetActive(false);
                del--;
                if (del == 0) return ;      
            }
    }
    public float GetSquadRadiusActive()
    {
        int k = 0;
        foreach (Transform c in transform)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return  0.3f*Mathf.Sqrt(k);
    }

    

    
}
