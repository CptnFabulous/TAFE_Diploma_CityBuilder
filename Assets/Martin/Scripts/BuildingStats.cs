using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStats : MonoBehaviour
{
    public GameObject buildingmanagement;

    private void Start()
    {
        buildingmanagement = GameObject.FindGameObjectWithTag("BuildingManagement");
    }
}
