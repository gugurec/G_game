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
