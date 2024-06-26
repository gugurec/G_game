using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CameraSettings cameraSettings;

    private Animation cameraAnimation;
    private bool isDragging = false;
    private Vector3 startDraggingCameraPos;
    private Vector3 startDraggingMousePos;

    public void MoveCamera(Map.MapPos mapPos)
    {
        MoveCamera2D(new Vector2(mapPos.worldPos.x, mapPos.worldPos.y));
    }
    private void MoveCamera2D(Vector2 target)
    {
        float yOffset = Mathf.Abs(transform.position.z) * Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad);
        Vector3 pos = new Vector3(target.x, target.y + yOffset, transform.position.z);
        MoveCamera(pos);
    }

    private void MoveCamera(Vector3 target)
    {
        Keyframe[] keyframesX = new Keyframe[2];
        Keyframe[] keyframesY = new Keyframe[2];
        Keyframe[] keyframesZ = new Keyframe[2];

        keyframesX[0] = new Keyframe(0f, transform.position.x);
        keyframesX[1] = new Keyframe(cameraSettings.cameraMoveToPointAnimTime, target.x);
        AnimationCurve animationCurveX = new AnimationCurve(keyframesX);

        keyframesY[0] = new Keyframe(0f, transform.position.y);
        keyframesY[1] = new Keyframe(cameraSettings.cameraMoveToPointAnimTime, target.y);
        AnimationCurve animationCurveY = new AnimationCurve(keyframesY);

        keyframesZ[0] = new Keyframe(0f, transform.position.z);
        keyframesZ[1] = new Keyframe(cameraSettings.cameraMoveToPointAnimTime, target.z);
        AnimationCurve animationCurveZ = new AnimationCurve(keyframesZ);

        AnimationClip cameraAnimationClip = new AnimationClip();
        
        cameraAnimationClip.SetCurve("", typeof(UnityEngine.Transform), "localPosition.x", animationCurveX);
        cameraAnimationClip.SetCurve("", typeof(UnityEngine.Transform), "localPosition.y", animationCurveY);
        cameraAnimationClip.SetCurve("", typeof(UnityEngine.Transform), "localPosition.z", animationCurveZ);
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
        //needStopAnimation = needStopAnimation || CameraBorderMove();
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
        if (Input.mousePosition.y >= Screen.height * (1 - cameraSettings.borderThicknessPercent))
        {
            isMove = true;
            pos.y += cameraSettings.borderMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= Screen.height * cameraSettings.borderThicknessPercent)
        {
            isMove = true;
            pos.y -= cameraSettings.borderMoveSpeed * Time.deltaTime;
        }
        //left-right
        if (Input.mousePosition.x >= Screen.width * (1 - cameraSettings.borderThicknessPercent))
        {
            isMove = true;
            pos.x += cameraSettings.borderMoveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= Screen.width * cameraSettings.borderThicknessPercent)
        {
            isMove = true;
            pos.x -= cameraSettings.borderMoveSpeed * Time.deltaTime;
        }

        transform.position = pos;
        return isMove;
    }

    private bool CameraDraggingMove()
    {
        if (Input.GetMouseButtonDown(2))
        {
            startDraggingCameraPos = transform.position;
            startDraggingMousePos = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButton(2))
        {
            float vizibleGameFieldWidth = Mathf.Abs(transform.position.z) / Mathf.Cos(GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad / 2);
            float gameFieldToScreenPosY = vizibleGameFieldWidth / Screen.height;
            Vector3 deltaMousePos = startDraggingMousePos - Input.mousePosition;

            transform.position = startDraggingCameraPos + deltaMousePos * gameFieldToScreenPosY;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isDragging = false;
        }
        return isDragging;
    }
}
