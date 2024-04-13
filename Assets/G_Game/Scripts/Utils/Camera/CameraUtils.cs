using UnityEngine;

public class CameraUtils
{
    private const float OFFSET_Y = 4.5f;

    static public void LookAt(Tile tile)
    {
        Vector3 pos = new Vector3(tile.transform.position.x, tile.transform.position.y - OFFSET_Y);
        Camera.main.transform.position = pos;
    }
}
