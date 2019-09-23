using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    //public int towerMax = 2;
    public int buildingIndex = 1;
    public int placeableCheck = 0;

    public Vector3 boxSize;
    public Vector3 boxPosition;
    public LayerMask ground;

    public void Awake()
    {
        placeableCheck = 0;
        boxPosition = new Vector3(1, 0, 1);
        boxSize = new Vector3(2, 2, 2);
    }

    public void SpawnBuilding(Vector3 position)
    {
        GameObject buildingPrefab = Resources.Load<GameObject>("Prefabs/Buildings " + buildingIndex);
        Instantiate(buildingPrefab, position, Quaternion.identity);
    }

    void Update()
    {

        //if (Input.GetMouseButtonDown(1))
        //{
        //    towerIndex++;
        //}
        //if (towerIndex > towerMax)
        //{
        //    towerIndex = 1;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit))
            {
                Placeable p = hit.collider.GetComponent<Placeable>();

                Collider[] placementHits = Physics.OverlapBox(boxSize, boxPosition, Quaternion.identity, ground);

                if (p)
                {
                    for (int i = 0; i < placementHits.Length; i++)
                    {
                        if (placementHits[i].gameObject.GetComponent<Placeable>().isOccupied == false)
                        {
                            placeableCheck++;
                        }
                    }

                    if (placeableCheck == 4)
                    {
                        SpawnBuilding(hit.transform.position);
                        for (int i = 0; i < placementHits.Length; i++)
                        {
                            placementHits[i].gameObject.GetComponent<Placeable>().isOccupied = true;
                        }

                    }
                    else
                    {
                        placeableCheck = 0;
                        Debug.Log("Can't place here, not enough space");
                    }
                    
                }
            }
        }


    }
}
