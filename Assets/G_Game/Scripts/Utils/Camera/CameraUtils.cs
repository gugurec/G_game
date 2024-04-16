using UnityEngine;

public class CameraUtils
{
    static public void LookAt(Tile tile)
    {
        Vector2 pos = new Vector2(tile.transform.position.x, tile.transform.position.y);
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController)
        {
            cameraController.MoveCamera2D(pos);
        }
    }
}
