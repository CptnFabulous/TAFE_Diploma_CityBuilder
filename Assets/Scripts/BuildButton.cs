using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private UnityEvent _onPressed = null;

    private Building _buildingData = null;

    [SerializeField]
    private Text _buildingName = null;

    [SerializeField]
    private RawImage _buildingSprite = null;

    #endregion

    #region Public Properties

    public Building BuildingData
    {
        get { return _buildingData; }
    }

    #endregion

    #region MonoBehaviour Functions

    #endregion

    #region Public Functions

    public void Press()
    {
        _onPressed.Invoke();
    }

    public void Setup(Building buildingData)
    {
        _buildingData = buildingData;

        _buildingName.text = _buildingData.BuildingName;
        _buildingSprite.texture = _buildingData.BuildingSprite.texture;
    }

    #endregion
}
