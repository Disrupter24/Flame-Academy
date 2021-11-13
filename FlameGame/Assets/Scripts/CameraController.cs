
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
    
    // Update is called once per frame
    void Update()
    {
        if (!CanMove) return;
        Vector3 cameraPosition = transform.position;
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - PanBorderThickness)
        {
            cameraPosition.y += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= PanBorderThickness)
        {
            cameraPosition.y -= PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= PanBorderThickness)
        {
            cameraPosition.x -= PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            cameraPosition.x += PanSpeed * Time.deltaTime;
        }
        /*if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Mouse 2 ");
        }*/

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
