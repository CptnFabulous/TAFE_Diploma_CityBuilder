using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    int current;
    int max;
}

public class ResourceManager : MonoBehaviour
{



    public int money;
    public int income;
    public int spending;
    public bool inDebt;

    public int energyGeneration;
    public int energyConsumption;

    public int population;
    [Range(0, 100)]
    public float happiness;
    
    bool InDebt()
    {
        if (money > 0)
        {
            return false;
        }

        // Do stuff when in debt
        return true;
    }

    bool SpendMoney(int moneyToSpend)
    {
        if (money >= moneyToSpend)
        {
            money -= moneyToSpend;
            return true;
        }
        return false;
    }

    void SpendBudget()
    {
        money -= spending;
    }

    void Income(int income)
    {
        money += income;
    }
}
