using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float panSpeed = 2f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomInMax = 40f;
    [SerializeField] private float zoomOutMax = 90f;

    private PlayerControls controls;
    
    private Transform cameraTransform;

    void Awake()
    {
        controls = new PlayerControls();
        //controls.Gameplay.ZoomIn.started, performed, canceled
        // controls.Gameplay.ZoomIn.started += ctx => ZoomScreen(new Vector2(0, 1f));
        // controls.Gameplay.ZoomOut.started += ctx => ZoomScreen(new Vector2(0, -1f));
    }
    
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Pan();
        Zoom();
    }

    private void Pan()
    {
        var cameraPos = cameraTransform.position;
        var mousePanDir = PanDirection(Input.mousePosition.x, Input.mousePosition.y);
        var gamepadInput = controls.Gameplay.Pan.ReadValue<Vector2>();
        if (mousePanDir.x != 0 || mousePanDir.y != 0)
        {
            var end = cameraPos + (Vector3)mousePanDir * panSpeed;
            cameraTransform.position = Vector3.Lerp(cameraPos, end, Time.deltaTime);
        } else if (gamepadInput.x != 0 || gamepadInput.y != 0)
        {
            var end = cameraPos + (Vector3)gamepadInput * panSpeed;
            cameraTransform.position = Vector3.Lerp(cameraPos, end, Time.deltaTime);
        }
        else
        {
            var playerPos = playerTransform.position;
            var end = new Vector3(playerPos.x, playerPos.y, cameraPos.z);
            cameraTransform.position = Vector3.MoveTowards(cameraPos, end, Time.deltaTime * panSpeed);
        }
    }

    private Vector2 PanDirection(float x, float y) {
        var direction = Vector2.zero;
        if (y >= Screen.height * 1f) {
            direction.y += 1f;
        } else if (y <= Screen.height * 0f) {
            direction.y -= 1f;
        }	
        if (x >= Screen.width * 1f) {
            direction.x += 1f;
        }
        else if (x <= Screen.width * 0f) {
            direction.x -= 1f;
        }
        return direction;
    }

    private void Zoom()
    {
        ZoomScreen(Input.mouseScrollDelta);
        if (controls.Gameplay.ZoomOut.IsPressed())
        {
            ZoomScreen(new Vector2(0, -0.1f));
        }
        else if (controls.Gameplay.ZoomIn.IsPressed())
        {
            ZoomScreen(new Vector2(0, 0.1f));
        }
    }

    private void ZoomScreen(Vector2 inputDelta)
    {
        float scrollAmount = inputDelta.y * zoomSpeed;
        if (scrollAmount != 0)
        {
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scrollAmount, zoomInMax, zoomOutMax);
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
