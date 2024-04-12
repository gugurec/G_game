using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    [SerializeField]
    private MapCreationSettings generationSettings;
    public void CreateMap()
    {
        Debug.Log("Creating map...");
        if (!generationSettings.Validate())
        {
            Debug.Log("Creating map fail");
            return;
        }

        ClearMap();

        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;
        GameObject tile = Instantiate(generationSettings.prefab, position, rotation);

        tile.transform.parent = transform;
    }

    public void ClearMap()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Application.isEditor)
                DestroyImmediate(transform.GetChild(i).gameObject);
            else
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
