using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Map;

public class MapUtils
{
    public static List<Vector3Int> MapPosToCellPos(List<MapPos> mapPositions)
    {
        List<Vector3Int> result = new List<Vector3Int>();
        foreach (MapPos mapPos in mapPositions)
        {
            result.Add(mapPos.cellPos);
        }
        return result;
    }
}
