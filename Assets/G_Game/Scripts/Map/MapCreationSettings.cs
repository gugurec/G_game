using UnityEngine;

[System.Serializable]
public class MapCreationSettings
{
    public enum MapCreationType
    {
        Sqare,
        Circle,
        Maze
    }

    public GameObject prefab;
    public int size;
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

        if(!isValid) { 
            Debug.LogError(errors);
        }
        return isValid;
    }
}
