using UnityEngine;

public class CameraUtils
{
    public static Vector3 Screen2World(Vector3 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float t = -ray.origin.z / ray.direction.z;
        Vector3 intersectionPoint = ray.origin + ray.direction * t;
        return intersectionPoint;
    }
}
