using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDeleter : MonoBehaviour
{
    #region Private Variables

    private bool _active = false;

    [SerializeField]
    private GameObject _selectionMarker = null;

    [SerializeField]
    private Renderer _selectionRenderer = null;

    private Material _goodSelection = null;

    [SerializeField]
    private Material _badSelection = null;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        _goodSelection = _selectionRenderer.material;
    }

    private void Update()
    {
        if (!_active)
        {
            _selectionMarker.SetActive(false);
            return;
        }

        MapTile tile = null;
        RaycastHit hit;
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hit))
        {
            tile = hit.collider.GetComponentInParent<MapTile>();
        }

        _selectionMarker.SetActive(null != tile);
        if (null != tile)
        {
            BuildingObject currentBuilding = tile.OccupyingBuilding;

            if (null != currentBuilding)
            {
                Building currentBuildingData = currentBuilding.BuildingData;

                _selectionMarker.transform.localScale = new Vector3(currentBuildingData.Size.x - 0.05f, currentBuildingData.Size.y - 0.05f, 0.1f);

                Vector3 positionSizeAdjust = Quaternion.Euler(45f, 0f, 45f) * Vector3.up * (float)(currentBuildingData.Size.y - 1) / 2f;
                positionSizeAdjust += Quaternion.Euler(45f, 0f, 45f) * Vector3.right * (float)(currentBuildingData.Size.x - 1) / 2f;
                _selectionMarker.transform.position = currentBuilding.BottomTile.transform.position + Quaternion.Euler(45f, 0f, 45f) * Vector3.back * 0.1f + positionSizeAdjust;
                _selectionRenderer.material = _goodSelection;

                if (Input.GetMouseButtonDown(0))
                {
                    tile.RemoveBuilding();
                }
            }
            else
            {
                _selectionMarker.transform.localScale = new Vector3(0.95f, 0.95f, 0.1f);
                _selectionMarker.transform.position = hit.collider.transform.position + Quaternion.Euler(45f, 0f, 45f) * Vector3.back * 0.1f;
                _selectionRenderer.material = _badSelection;
            }
        }

        if (Input.GetMouseButton(1))
        {
            _active = false;
        }
    }

    #endregion

    #region Public Functions

    public void StartDeletion()
    {
        _active = true;
    }

    public void StopDeletion()
    {
        _active = false;
    }

    #endregion
}
