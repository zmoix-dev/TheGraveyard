using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    [SerializeField] float zoomInFov = 10f;
    [SerializeField] float zoomOutFov = 40f;
    [SerializeField] float zoomMouseModifier = 0.5f;

    FirstPersonController fpsController;


    bool isZoomed = false;

    private void Start() {
        fpsController = GetComponent<FirstPersonController>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            HandleZoomToggle();
        }
    }

    void HandleZoomToggle() {
        isZoomed = !isZoomed;
        if (isZoomed) {
            mainCamera.fieldOfView = zoomInFov;
            fpsController.RotationSpeed = fpsController.RotationSpeed * zoomMouseModifier;
        } else {
            mainCamera.fieldOfView = zoomOutFov;
            fpsController.RotationSpeed = fpsController.RotationSpeed / zoomMouseModifier;
        }
    }
}
