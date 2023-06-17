using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] FirstPersonController fpsController;

    [SerializeField] float zoomInFov = 10f;
    [SerializeField] float zoomOutFov = 40f;
    [SerializeField] float zoomInMouseSpeed = 0.5f;
    [SerializeField] float zoomOutMouseSpeed = 0.5f;

    bool isZoomed = false;

    private void OnDisable(){
        ZoomOut();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HandleZoomToggle();
        }
    }

    void HandleZoomToggle() {
        isZoomed = !isZoomed;
        if (isZoomed)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomOut()
    {
        mainCamera.fieldOfView = zoomOutFov;
        fpsController.RotationSpeed = zoomOutMouseSpeed;
    }

    private void ZoomIn()
    {
        mainCamera.fieldOfView = zoomInFov;
        fpsController.RotationSpeed = zoomInMouseSpeed;
    }
}
