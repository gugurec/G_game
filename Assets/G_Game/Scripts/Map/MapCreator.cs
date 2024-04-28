using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static MapCreationSettings;

public class MapCreator : MonoBehaviour
{
    [SerializeField]
    private AnimatedTile tile;
    [SerializeField]
    private Tilemap tileMap;
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
        SpawnTiles();
    }

    public void ClearMap()
    {
        tileMap.ClearAllTiles();
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
        for (int x = 0; x < generationSettings.size; x++)
        {
            for (int y = 0; y < generationSettings.size; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tileMap.SetTile(tilePosition, tile);
                tileMap.SetAnimationFrame(tilePosition, 0);
            }
        }
    }
}
