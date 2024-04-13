using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    const float OFFSET = 0.8660254f;

    [SerializeField]
    [ReadOnly]
    private Vector2Int position;
    public Vector2Int Position
    {
        set { position = value; }
    }
    public void UpdatePosition(float distance)
    {
        Vector3 pos;
        pos = new Vector3(distance * position.x * 1.5f, distance * position.y * OFFSET);
        transform.position = pos;
    }
}
