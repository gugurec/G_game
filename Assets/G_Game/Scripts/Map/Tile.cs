using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    const float OFFSET_Y = 0.8660254f;//    sqrt(3)/2
    const float OFFSET_X = 1.5f;

    [SerializeField]
    private GameObject sprite;

    [SerializeField]
    [ReadOnly]
    private Vector2Int position;
    public Vector2Int Position
    {
        set { position = value; }
    }
    public void UpdatePosition()
    {
        SpriteRenderer spriteRendererComponent = sprite.GetComponent<SpriteRenderer>();
        if (spriteRendererComponent != null)
        {
            float distance = spriteRendererComponent.bounds.size.x / 2;
            Vector3 pos = new Vector3(distance * position.x * OFFSET_X, distance * position.y * OFFSET_Y);
            transform.position = pos;
        }
        
    }
}
