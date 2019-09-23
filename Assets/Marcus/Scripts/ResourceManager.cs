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
    public GameManager gm;

    public int money;
    public bool inDebt;

    public int population;
    [Range(0, 100)]
    public float happiness;

    public List<buildings> buildings;

    public bool InDebt()
    {
        if (money > 0)
        {
            return false;
        }

        // Do stuff when in debt
        return true;
    }

    public int Income(List<buildings> buildings)
    {
        int i = 0;
        foreach (buildings b in buildings)
        {
            i += b.income;
        }
        return i;
    }

    public int Spending(List<buildings> buildings)
    {
        int i = 0;
        foreach (buildings b in buildings)
        {
            i += b.maintenanceCost;
        }
        return i;
    }

    public int EnergyConsumption(List<buildings> buildings)
    {
        int i = 0;
        foreach (buildings b in buildings)
        {
            i += b.energyUsage;
        }
        return i;
    }

    public int EnergyProduction(List<buildings> buildings)
    {
        int i = 0;
        foreach (buildings b in buildings)
        {
            i += b.energyProduction;
        }
        return i;
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

    void SpendBudget(List<buildings> buildings)
    {
        money -= Spending(buildings);
    }

    void Income(int income)
    {
        money += income;
    }
}
