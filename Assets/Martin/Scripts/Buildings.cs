using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Buildings
{
    [System.Serializable]
    public struct buildings
    {
        public string name;
        public int purchaseCost;
        public int energyProduction;
        public int energyUsage;
        public int maintenanceCost;
        public int income;
        public int population;
        public int happiness;
        public BuildingTypes type;
    };
}
public enum BuildingTypes
{
    Power,
    Shop,
    Housing,
    Entertainment
}
