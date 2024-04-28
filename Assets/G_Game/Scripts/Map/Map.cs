using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;
    private List<Coroutine> animations = new List<Coroutine>();
    private static int radius = 10;
    private void Start()
    {
        ResetMap();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = CameraUtils.Screen2World(Input.mousePosition);
            AnimateMap(pos);
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
    private void AnimateMap(Vector3 pos)
    {
        BoundsInt bounds = tileMap.cellBounds;
        Vector3Int centerPos = tileMap.WorldToCell(pos);
        if (bounds.Contains(centerPos))
        {
            animations.ForEach(anim => { StopCoroutine(anim); });
            animations.Clear();
            ResetMap();
            for (int x = centerPos.x - radius; x < centerPos.x + radius; x++)
            {
                for (int y = centerPos.y - radius; y < centerPos.y + radius; y++)
                {
                    var currentPos = new Vector3Int(x, y, 0);
                    float distance = Mathf.Abs(Vector3Int.Distance(centerPos, currentPos));
                    float timeOffset = distance * 0.05f;
                    animations.Add(StartCoroutine(AnimateTile(tileMap, currentPos, timeOffset)));
                }
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
