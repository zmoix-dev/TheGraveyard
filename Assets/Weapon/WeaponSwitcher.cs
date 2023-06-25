using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    private void Start() {
        SetWeaponActive();
    }

    private void Update() {
        int previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheelInput();
        if (previousWeapon != currentWeapon) {
            SetWeaponActive();
        }
    }

    private void ProcessScrollWheelInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (currentWeapon >= transform.childCount - 1) {
                currentWeapon = 0;
            } else {
                currentWeapon++;
            }
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0 ){
            if (currentWeapon <= 0) {
                currentWeapon = transform.childCount - 1;
            } else {
                currentWeapon--;
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentWeapon = 1;
        }
    }

    private void SetWeaponActive()
    {
        for (int i = 0; i < transform.childCount; i++) {
            Transform weapon = transform.GetChild(i);
            if (i == currentWeapon) {
                weapon.gameObject.SetActive(true);
            } else {
                weapon.gameObject.SetActive(false);
            }
            
        }
    }
}
