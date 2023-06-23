using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootChest : MonoBehaviour
{
    [SerializeField] int minMedAmmoQty = 4;
    [SerializeField] int maxMedAmmoQty = 20;
    [SerializeField] int minLargeAmmoQty = 1;
    [SerializeField] int maxLargeAmmoQty = 6;
    [SerializeField] float minBatteryRechargePct = 0.25f;
    [SerializeField] float maxBatteryRechargePct = 0.75f;

    [SerializeField] float lootableRange = 5f;
    [SerializeField] GameObject lootText;
    [SerializeField] bool isKeyChest = false;
    bool isLooted = false;
    GameObject player;
    Ammunition ammunitionSystem;
    Flashlight flashlight;

    void Start() {
        player = GameObject.FindWithTag("Player");    
        ammunitionSystem = player.GetComponent<Ammunition>();
        flashlight = player.GetComponentInChildren<Flashlight>();
    }

    // Update is called once per frame
    void Update() {
        if (isLooted) {
            return;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < lootableRange) {
            lootText.SetActive(true);
            HandlePickup();
        } else {
            lootText.SetActive(false);
        }
    }

    private void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            isLooted = true;
            int medAmmo = UnityEngine.Random.Range(minMedAmmoQty, maxMedAmmoQty);
            int largeAmmo = UnityEngine.Random.Range(minLargeAmmoQty, maxLargeAmmoQty);
            float rechargePct = UnityEngine.Random.Range(minBatteryRechargePct, maxBatteryRechargePct);
            ammunitionSystem.Increase(AmmunitionType.Medium, medAmmo);
            ammunitionSystem.Increase(AmmunitionType.Large, largeAmmo);
            flashlight.RechargeBattery(rechargePct);

            Destroy(gameObject);
        }
    }
}
