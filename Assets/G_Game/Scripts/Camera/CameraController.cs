using UnityEngine;
using System.Collections;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public void MoveCamera(Vector3 target, float duration)
    {
        Keyframe[] keyframesX = new Keyframe[2];
        Keyframe[] keyframesY = new Keyframe[2];
        Keyframe[] keyframesZ = new Keyframe[2];

        keyframesX[0] = new Keyframe(0f, transform.position.x);
        keyframesX[1] = new Keyframe(duration, target.x);
        AnimationCurve animationCurveX = new AnimationCurve(keyframesX);

        keyframesY[0] = new Keyframe(0f, transform.position.y);
        keyframesY[1] = new Keyframe(duration, target.y);
        AnimationCurve animationCurveY = new AnimationCurve(keyframesY);

        keyframesZ[0] = new Keyframe(0f, transform.position.z);
        keyframesZ[1] = new Keyframe(1f, target.z);
        AnimationCurve animationCurveZ = new AnimationCurve(keyframesZ);

        AnimationClip cameraAnimationClip = new AnimationClip();
        
        cameraAnimationClip.SetCurve("", typeof(Transform), "localPosition.x", animationCurveX);
        cameraAnimationClip.SetCurve("", typeof(Transform), "localPosition.y", animationCurveY);
        cameraAnimationClip.SetCurve("", typeof(Transform), "localPosition.z", animationCurveZ);
        cameraAnimationClip.legacy = true;

        Animation cameraAnimation = GetComponent<Animation>();
        if (cameraAnimation)
        {
            cameraAnimation.AddClip(cameraAnimationClip, "CameraMoveAnimation");
            cameraAnimation.Play("CameraMoveAnimation");
        }
    }
}
