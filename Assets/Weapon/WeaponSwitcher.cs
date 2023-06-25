using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] Weapon[] weapons;

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
            if (currentWeapon >= weapons.Length - 1) {
                currentWeapon = 0;
            } else {
                currentWeapon++;
            }
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0 ){
            if (currentWeapon <= 0) {
                currentWeapon = weapons.Length - 1;
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
        for (int i = 0; i < weapons.Length; i++) {
            if (i == currentWeapon) {
                weapons[i].transform.gameObject.SetActive(true);
                weapons[i].SetImageAlpha(1f);
            } else {
                weapons[i].transform.gameObject.SetActive(false);
                weapons[i].SetImageAlpha(0.5f);
            }
        }
    }
}
