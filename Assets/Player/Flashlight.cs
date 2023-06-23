using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float maxIntensity = 10f;
    [SerializeField] float minAngle = 10f;
    [SerializeField] float maxAngle = 30f;

    Light flashlight;

    void Start() {
        flashlight = GetComponent<Light>();
    }

    void Update() {
        DecreaseLightIntensity();
        DecreaseLightAngle();
    }


    private void DecreaseLightIntensity()
    {
        if (flashlight.intensity > 0) {
            flashlight.intensity -= lightDecay * Time.deltaTime;
        }
    }
    private void DecreaseLightAngle()
    {
        if (flashlight.spotAngle > minAngle) {
            flashlight.spotAngle = (flashlight.intensity / maxIntensity) * (maxAngle - minAngle) + minAngle;
        }
    }
    
    public void RechargeBattery(float chargePct) {
        float chargeAmt = chargePct * maxIntensity;
        flashlight.intensity = Math.Min(flashlight.intensity + chargeAmt, maxIntensity);
        Debug.Log($"Added {chargePct} to battery.");
    }
}