using UnityEngine;
using static MapCreationSettings;

public class MapCreator : MonoBehaviour
{
    [SerializeField]
    private MapCreationSettings generationSettings;

    private void OnValidate()
    {
        UpdateTilesPosition();
    }
    public void CreateMap()
    {
        Debug.Log("Creating map...");
        if (!generationSettings.Validate())
        {
            Debug.Log("Creating map fail");
            return;
        }

        ClearMap();
        SpawnTiles();
    }

    public void ClearMap()
    {
        while (transform.childCount != 0)
        {
            if (Application.isEditor)
                DestroyImmediate(transform.GetChild(0).gameObject);
            else
                Destroy(transform.GetChild(0).gameObject);
        }
    }
    private void SpawnTiles()
    {
        switch (generationSettings.mapGenerationType)
        {
            case MapCreationType.Sqare:
                SpawnSqare();
                break;
            case MapCreationType.Circle:
                Debug.LogError("Not realized MapCreationType");
                break;
            case MapCreationType.Maze:
                Debug.LogError("Not realized MapCreationType");
                break;
            default:
                Debug.LogError("Unknown MapCreationType");
                break;
        }
    }
    private void SpawnSqare()
    {
        for (int i = 0; i < generationSettings.size; i++)
        {
            for (int j = 0; j < generationSettings.size; j++)
            {
                if((i + j) % 2 == 0)//если оба индекса четные или не четные
                    SpawnTile(i, j);
            }
        }
    }
    private void SpawnTile(int x, int y)
    {
        GameObject tile = Instantiate(generationSettings.prefab, transform);
        Tile tileBehaviour = tile.GetComponent<Tile>();
        tileBehaviour.Position = new Vector2Int(x, y);
        tileBehaviour.UpdatePosition();
    }
    private void UpdateTilesPosition()
    {

        for(int i=0; i< transform.childCount; i++)
        {
            Tile tile = transform.GetChild(i).GetComponent<Tile>();
            if(tile != null)
            {
                tile.UpdatePosition();
            }
        }
    }
}
