
using UnityEngine;

public static class BonusUtils
{
    public enum BonusType { Add, Multiply, Sub }

    
    public static int GetRunnersAmountToAdd(int currentRunnersAmount, Bonus bonus)
    {
        switch(bonus.GetBonusType())
        {
            case BonusType.Add:
                return bonus.GetValue();
            case BonusType.Sub:
                return -bonus.GetValue();

            case BonusType.Multiply:
                return currentRunnersAmount*bonus.GetValue() - currentRunnersAmount;
        }

        return 0;
    }

    public static string GetBonusString(Bonus bonus)
    {
        string bonusString = null;

        switch(bonus.GetBonusType())
        {
            case BonusType.Add:
                bonusString += "+";
                break;

            case BonusType.Multiply:
                bonusString += "x";
                break;
            case BonusType.Sub:
                bonusString += "-";
                break;
        }

        bonusString += bonus.GetValue();

        return bonusString;
    }

   
}

[System.Serializable]
public struct Bonus
{
    [SerializeField] private BonusUtils.BonusType bonusType;
    [SerializeField] private int value;

    public Bonus(BonusUtils.BonusType bonusType, int value)
    {
        this.bonusType = bonusType;
        this.value = value;
    }

    public BonusUtils.BonusType GetBonusType()
    {
        return bonusType;
    }

    public int GetValue()
    {
        return value;
    }
}
