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

    public int size;
    public MapCreationType mapGenerationType;
    public bool Validate() 
    {
        bool isValid = true;
        string errors = "";

        if(!isValid) { 
            Debug.LogError(errors);
        }
        return isValid;
    }
}
