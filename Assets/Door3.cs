
using UnityEngine;
using TMPro;

public class Door3 : MonoBehaviour
{
    [Header(" Bonuses ")]

  
    [SerializeField] private Bonus tBonus;


    [Header(" Components ")]
    [SerializeField] private Collider[] doorsColliders;
 //   [SerializeField] private TextMeshPro fText;
  //  [SerializeField] private TextMeshPro sText;
    [SerializeField] private TextMeshPro tText;

    // Start is called before the first frame update
    void Start()
    {


        ConfigureBonusTexts();
    }

    private void ConfigureBonusTexts()
    {
       // fText.text = BonusUtils.GetBonusString(fBonus);
      //  sText.text = BonusUtils.GetBonusString(sBonus);
        tText.text = BonusUtils.GetBonusString(tBonus);
    }

    public int GetRunnersAmountToAdd( int currentRunnersAmount)
    {
        DisableDoors();

        Bonus bonus;

     //   if (collidedDoor.transform.position.x > 0)
     //       bonus = fBonus;
     //   else if (collidedDoor.transform.position.x < 0)
     //       bonus = sBonus;
    //    else
            bonus = tBonus;


        return BonusUtils.GetRunnersAmountToAdd(currentRunnersAmount, bonus);
    }

    private void DisableDoors()
    {
        foreach (Collider c in doorsColliders)
            c.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
