using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float panSpeed = 2f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomInMax = 40f;
    [SerializeField] private float zoomOutMax = 90f;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = mainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PanScreen(Input.mousePosition.x, Input.mousePosition.y);
        ZoomScreen(Input.mouseScrollDelta);
    }
    
    private void PanScreen(float x, float y) {
        var cameraPos = cameraTransform.position;
        var dir = PanDirection(x, y);
        if (dir.x != 0 || dir.y != 0)
        {
            var end = cameraPos + (Vector3)dir * panSpeed;
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

    private void ZoomScreen(Vector2 scrollDelta)
    {
        float scrollAmount = scrollDelta.y * zoomSpeed;
        if (scrollAmount != 0)
        {
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - scrollAmount, zoomInMax, zoomOutMax);
        }
    }

}
