
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera OrtographicCamera;
    public float PanSpeed = 20f;
    public float PanBorderThickness = 10f;
    public float ScrollSpeed = 20f;
    public float MinCameraSize, MaxCameraSize;
    public Vector2 panLimit;
    public bool CanMove = false;
    public bool isScrolling = false; 
    // Update is called once per frame
    void Update()
    {
        if (!CanMove) return;
        Vector3 cameraPosition = transform.position;

        bool isStillScrolling = false;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - PanBorderThickness)
        {
            cameraPosition.y += PanSpeed * Time.deltaTime;
            UIAction.OnCursorScroll(UICursorManager.Direction.N);
            isStillScrolling = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.mousePosition.y <= PanBorderThickness)
        {
            cameraPosition.y -= PanSpeed * Time.deltaTime;
            UIAction.OnCursorScroll(UICursorManager.Direction.S);
            isStillScrolling = true;

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.mousePosition.x <= PanBorderThickness)
        {
            cameraPosition.x -= PanSpeed * Time.deltaTime;
            UIAction.OnCursorScroll(UICursorManager.Direction.W);
            isStillScrolling = true;

        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            cameraPosition.x += PanSpeed * Time.deltaTime;
            UIAction.OnCursorScroll(UICursorManager.Direction.E);
            isStillScrolling = true;

        }

        if (isScrolling && !isStillScrolling)
        {
            isScrolling = false;
            UIAction.OnCursorScrollStop();
        } else if (isStillScrolling)
        {
            isScrolling = true;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        OrtographicCamera.orthographicSize -= scroll * ScrollSpeed * 100f * Time.deltaTime;
        //cameraPosition.z += scroll * ScrollSpeed * 100f * Time.deltaTime;

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -panLimit.x, panLimit.x);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, -panLimit.y, panLimit.y);
        OrtographicCamera.orthographicSize = Mathf.Clamp(OrtographicCamera.orthographicSize, MinCameraSize, MaxCameraSize);



        transform.position = cameraPosition;
    }
    public void AllowCameraMovement()
    {
        CanMove = !CanMove;
    }
}
