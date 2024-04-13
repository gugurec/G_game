using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private PolygonCollider2D hexagonCollider;
    [SerializeField]
    [ReadOnly]
    private Vector2Int position;

    private const float OFFSET_Y = 0.8660254f;//    sqrt(3)/2
    private const float OFFSET_X = 1.5f;

    private bool isHighLight = false;
    public Vector2Int Position
    {
        set { position = value; }
    }
    public void UpdatePosition()
    {
        float distance = hexagonCollider.bounds.size.x / 2;
        Vector3 pos = new Vector3(distance * position.x * OFFSET_X, distance * position.y * OFFSET_Y);
        transform.position = pos;
    }

    public void SwitchHighLight()
    {
        if (isHighLight)
        {
            sprite.color = Color.white;
        }
        else
        {
            sprite.color = Color.red;
        }
        isHighLight = !isHighLight;
    }

    private void OnMouseUpAsButton()
    {
        CameraUtils.LookAt(this);
    }
}
