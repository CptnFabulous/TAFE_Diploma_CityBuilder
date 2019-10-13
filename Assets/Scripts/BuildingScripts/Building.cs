using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : ScriptableObject
{
    #region Private Variables

    [SerializeField]
    private string _buildingName = "Building";

    [SerializeField]
    private Sprite _buildingSprite = null;

    [SerializeField]
    private Vector2Int _size = new Vector2Int(1, 1);

    #endregion

    #region Public Properties

    public string BuildingName
    {
        get { return _buildingName; }
    }

    public Sprite BuildingSprite
    {
        get { return _buildingSprite; }
    }

    public Vector2Int Size
    {
        get { return _size; }
    }

    #endregion
}
