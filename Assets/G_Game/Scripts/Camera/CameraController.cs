using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animation cameraAnimation;
    private bool isDragging = false;

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

        if (!cameraAnimation)
        {
            cameraAnimation = GetComponent<Animation>();
        }
        if (cameraAnimation)
        {
            cameraAnimation.AddClip(cameraAnimationClip, "CameraMoveAnimation");
            cameraAnimation.Play("CameraMoveAnimation");
        }
    }

    private void Start()
    {
        cameraAnimation = GetComponent<Animation>();
    }
    private void Update()
    {
        bool needStopAnimation = false;
        needStopAnimation = needStopAnimation || CameraBorderMove();
        needStopAnimation = needStopAnimation || CameraDraggingMove();
        if (needStopAnimation)
        {
            StopAnimation();
        }
    }
    private void StopAnimation()
    {
        if (cameraAnimation)
        {
            if (cameraAnimation.isPlaying)
            {
                cameraAnimation.Stop();
            }
        }
    }

    private bool CameraBorderMove()
    {
        Vector3 pos = transform.position;
        bool isMove = false;
        //up-down
        if (Input.mousePosition.y >= Screen.height * (1 - CameraUtils.BORDER_THICKNESS_PERCENT))
        {
            isMove = true;
            pos.y += CameraUtils.BORDER_MOVE_SPEED * Time.deltaTime;
        }
        if (Input.mousePosition.y <= Screen.height * CameraUtils.BORDER_THICKNESS_PERCENT)
        {
            isMove = true;
            pos.y -= CameraUtils.BORDER_MOVE_SPEED * Time.deltaTime;
        }
        //left-right
        if (Input.mousePosition.x >= Screen.width * (1 - CameraUtils.BORDER_THICKNESS_PERCENT))
        {
            isMove = true;
            pos.x += CameraUtils.BORDER_MOVE_SPEED * Time.deltaTime;
        }
        if (Input.mousePosition.x <= Screen.width * CameraUtils.BORDER_THICKNESS_PERCENT)
        {
            isMove = true;
            pos.x -= CameraUtils.BORDER_MOVE_SPEED * Time.deltaTime;
        }

        transform.position = pos;
        return isMove;
    }

    private bool CameraDraggingMove()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            //ToDo настроить скорость перемещения в зависимости от параметров камеры и ее расположения по оси Z.
            float mouseX = Input.GetAxis("Mouse X") * CameraUtils.DRAGGING_MOVE_SPEED * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * CameraUtils.DRAGGING_MOVE_SPEED * Time.deltaTime;

            transform.position = transform.position + new Vector3(-mouseX, -mouseY, 0);
        }
        return isDragging;
    }
}
