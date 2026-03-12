using UnityEngine;

public class MobileCameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 0.1f;

    [Header("Zoom")]
    public float zoomSpeed = 0.01f;
    public float minZoom = 10f;
    public float maxZoom = 40f;

    [Header("Map Limits")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    private Camera cam;

    private Vector2 lastTouchPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Application.isEditor)
        {
            HandleMouseMovement();
            HandleMouseZoom();
        }
        else
        {
            HandleTouchMovement();
            HandlePinchZoom();
        }

        ClampPosition();
    }

    void HandleTouchMovement()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;

                Vector3 move = new Vector3(-delta.x * moveSpeed, 0, -delta.y * moveSpeed);

                transform.Translate(move, Space.World);
            }
        }
    }

    void HandlePinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch finger1 = Input.GetTouch(0);
            Touch finger2 = Input.GetTouch(1);

            Vector2 prevPos1 = finger1.position - finger1.deltaPosition;
            Vector2 prevPos2 = finger2.position - finger2.deltaPosition;

            float prevDistance = (prevPos1 - prevPos2).magnitude;
            float currentDistance = (finger1.position - finger2.position).magnitude;

            float difference = currentDistance - prevDistance;

            Zoom(difference * zoomSpeed);
        }
    }

    void Zoom(float increment)
    {
        Vector3 pos = transform.position;

        pos.y -= increment * 10f;

        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);

        transform.position = pos;
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    void HandleMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            float moveX = -Input.GetAxis("Mouse X") * moveSpeed * 100f;
            float moveZ = -Input.GetAxis("Mouse Y") * moveSpeed * 100f;

            transform.Translate(new Vector3(moveX, 0, moveZ), Space.World);
        }
    }
    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            Zoom(scroll * 100f);
        }
    }
}