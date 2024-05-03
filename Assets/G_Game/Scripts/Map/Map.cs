using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Map;

public class Map : MonoBehaviour
{
    public delegate void OnTileClickEventHandler(MapPos mapPos);
    public event OnTileClickEventHandler OnTileClickEvent;

    [SerializeField]
    private Tilemap tileMap;
    private List<Coroutine> animations = new List<Coroutine>();
    private static int radius = 3;
    private List<Vector3Int> availableCellPositions = new List<Vector3Int>();

    public struct MapPos
    {
        public Vector3 worldPos;
        public Vector3Int cellPos;
        public MapPos(Vector3 worldPosition, Vector3Int cellPosition)
        {
            worldPos = worldPosition;
            cellPos = cellPosition;
        }
    }

    public List<MapPos> GetRandomTilesPos(in int count)
    {
        List<MapPos> positions = new List<MapPos>();
        for(int i = 0; i < count; i++)
        {
            var pos = GetRandomTilePosExclude(MapUtils.MapPosToCellPos(positions));
            positions.Add(pos);
        }
        return positions;
    }

    public MapPos GetRandomTilePosExclude(in List<Vector3Int> excludePos)
    {
        BoundsInt bounds = tileMap.cellBounds;
        //Сначала пробуем несколько раз взять случайную точку, в надежде что ее нет в excludePos
        int attempts = 3;
        while (attempts != 0)
        {
            Vector3Int randomPoint = new Vector3Int(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y));

            if (!excludePos.Contains(randomPoint))
            {
                return new MapPos(tileMap.GetCellCenterWorld(randomPoint), randomPoint);
            }

            attempts++;
        }
        //Случайная точка не нашлась, придется искать ее чуть дольше.
        List<Vector3Int> availableCellPositionsCopy = new List<Vector3Int>(availableCellPositions);
        availableCellPositionsCopy.RemoveAll(excludePos.Contains);
        if(availableCellPositionsCopy.Count > 0)
        {
            int randomIndex = Random.Range(0, availableCellPositionsCopy.Count);
            Vector3Int pos = availableCellPositionsCopy[randomIndex];
            return new MapPos(tileMap.GetCellCenterWorld(pos), pos);
        }
        else
        {
            Debug.LogError("Error while GetRandomTilePos...");
            return new MapPos(new Vector3(), new Vector3Int());
        }
        
    }
    public MapPos GetRandomTilePos()
    {
        BoundsInt bounds = tileMap.cellBounds;
        Vector3Int randomPoint = new Vector3Int(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        return new MapPos(tileMap.GetCellCenterWorld(randomPoint), randomPoint);
    }
    public void AnimateMap(MapPos pos)
    {
        animations.ForEach(anim => { StopCoroutine(anim); });
        animations.Clear();
        ResetMap();
        for (int x = pos.cellPos.x - radius; x < pos.cellPos.x + radius; x++)
        {
            for (int y = pos.cellPos.y - radius; y < pos.cellPos.y + radius; y++)
            {
                var currentPos = new Vector3Int(x, y, 0);
                float distance = Mathf.Abs(Vector3Int.Distance(pos.cellPos, currentPos));
                float timeOffset = distance * 0.05f;
                animations.Add(StartCoroutine(AnimateTile(tileMap, currentPos, timeOffset)));
            }
        }
    }
    private void Start()
    {
        ResetMap();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var worldPos = CameraUtils.Screen2World(Input.mousePosition);
            BoundsInt bounds = tileMap.cellBounds;
            Vector3Int cellPos = tileMap.WorldToCell(worldPos);
            if (bounds.Contains(cellPos))
            {
                OnTileClickEvent(new MapPos(tileMap.GetCellCenterWorld(cellPos), cellPos));
            }
        }
    }
    private void ResetMap()
    {
        BoundsInt bounds = tileMap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                tileMap.SetAnimationFrame(new Vector3Int(x, y, 0), 0);
            }
        }
        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tileMap.HasTile(pos))
            {
                availableCellPositions.Add(pos);
            }
        }
    }

    IEnumerator AnimateTile(Tilemap tileMap, Vector3Int pos, float timeOffset)
    {
        yield return new WaitForSeconds(timeOffset);
        tileMap.SetAnimationFrame(pos, 1);
        yield return new WaitForSeconds(0.1f);
        tileMap.SetAnimationFrame(pos, 2);
        yield return new WaitForSeconds(0.1f);
        tileMap.SetAnimationFrame(pos, 1);
        yield return new WaitForSeconds(0.1f);
        tileMap.SetAnimationFrame(pos, 0);
    }
}
