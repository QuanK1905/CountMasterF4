
using UnityEngine;
using TMPro;

public class SquadFormation : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private TextMeshPro squadAmountText;
    [SerializeField] private Transform bubble;
    [Header(" Formation Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float radiusFactor;
    [Range(0f, 1f)]
    [SerializeField] private float angleFactor;

  



    public void HideText()
    {
        bubble.gameObject.SetActive(false);
    }
   
   
    private void FixedUpdate()
    {
        
            FermatSpiralPlacement();
    }
    void Update()
    {
       
        squadAmountText.text = transform.childCount.ToString();
    }

    private void FermatSpiralPlacement()
    {
        float goldenAngle = 137.5f * angleFactor;  

        for (int i = 0; i < transform.childCount; i++)
        {
            float x = radiusFactor * Mathf.Sqrt(i+1) * Mathf.Cos(Mathf.Deg2Rad * goldenAngle * (i+1));
            float z = radiusFactor * Mathf.Sqrt(i+1) * Mathf.Sin(Mathf.Deg2Rad * goldenAngle * (i+1));

            Vector3 runnerLocalPosition = new Vector3(x, 0, z);
            transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).localPosition, runnerLocalPosition, 0.1f);
        }
    }

    public float GetSquadRadius()
    {
        return radiusFactor * Mathf.Sqrt(transform.childCount);
    }

   
   
}
