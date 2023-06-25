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
    Inventory inventory;
    LootManager lootManager;
    AudioSource sfx;

    void Start() {
        sfx = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");    
        ammunitionSystem = player.GetComponent<Ammunition>();
        flashlight = player.GetComponentInChildren<Flashlight>();
        inventory = player.GetComponent<Inventory>();
        lootManager = FindObjectOfType<LootManager>();
    }

    // Update is called once per frame
    void Update() {
        if (isLooted) {
            return;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < lootableRange) {
            lootText.SetActive(true);
            StartCoroutine(HandlePickup());
        } else {
            lootText.SetActive(false);
        }
    }

    private IEnumerator HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            isLooted = true;
            int medAmmo = UnityEngine.Random.Range(minMedAmmoQty, maxMedAmmoQty);
            int largeAmmo = UnityEngine.Random.Range(minLargeAmmoQty, maxLargeAmmoQty);
            float rechargePct = UnityEngine.Random.Range(minBatteryRechargePct, maxBatteryRechargePct);
            ammunitionSystem.Increase(AmmunitionType.Medium, medAmmo);
            ammunitionSystem.Increase(AmmunitionType.Large, largeAmmo);
            flashlight.RechargeBattery(rechargePct);
            if (isKeyChest) {
                inventory.AddKey();
            }
            lootManager.DisplayLoot(medAmmo, largeAmmo, rechargePct, isKeyChest);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            lootText.SetActive(false);
            if (sfx && !sfx.isPlaying) {
                sfx.Play();
            }
            while(sfx.isPlaying) {
                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
        }
    }
}
