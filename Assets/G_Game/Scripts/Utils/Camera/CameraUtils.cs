using UnityEngine;

public class CameraUtils
{
    private const float OFFSET_Y = 4.5f;
    private const float CONST_CAMERA_Z = -5;
    private const float CAMERA_MOVE_TIME = 0.5f;
    static public void LookAt(Tile tile)
    {
        Vector3 pos = new Vector3(tile.transform.position.x, tile.transform.position.y - OFFSET_Y, CONST_CAMERA_Z);
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController)
        {
            cameraController.MoveCamera(pos, CAMERA_MOVE_TIME);
        }
    }
}
