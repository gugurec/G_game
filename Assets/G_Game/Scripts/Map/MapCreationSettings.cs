using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class MapCreationSettings
{
    public enum MapCreationType
    {
        Rectengle,
        Circle,
        Maze
    }

    public GameObject prefab;
    public float tileDistance;
    public MapCreationType mapGenerationType;
    public bool Validate() 
    {
        bool isValid = true;
        string errors = "";

        if (!prefab)
        {
            isValid = false;
            errors += "tile prefab is empty\n";
        }
        if (tileDistance == 0)
        {
            isValid = false;
            errors += "tile distance = 0\n";
        }

        if(!isValid) { 
            Debug.LogError(errors);
        }
        return isValid;
    }
}
