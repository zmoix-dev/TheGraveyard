using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 12f;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammunition ammoSystem;
    [SerializeField] AmmunitionType ammoType;
    [SerializeField] float timeBetweenShots;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image image;
    [SerializeField] float lastEnabledAt = -1f;
    [SerializeField] AudioSource shootSfx;
    [SerializeField] AudioSource clickSfx;
    bool canShoot = true;
    GameObject vfxParent;
    
    void Start() {
        vfxParent = GameObject.FindWithTag("VfxParent");
    }

    void OnEnable() {
         if (lastEnabledAt != -1) {
            if (Time.time - lastEnabledAt > timeBetweenShots) {
                canShoot = true;
            } else {
                StartCoroutine(WaitForShootOnSwap());
            }
        }
    }

    private IEnumerator WaitForShootOnSwap() {
       yield return new WaitForSeconds(timeBetweenShots - (lastEnabledAt - Time.time));
       canShoot = true;
    }

    void OnDisable() {
        if (!canShoot) {
            lastEnabledAt = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        HandleFire();
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSystem.GetAmount(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    private void HandleFire()
    {
        if (Input.GetMouseButton(0)) {
            if (ammoSystem.GetAmount(ammoType) > 0 && canShoot) {
                 StartCoroutine(Shoot());
            } else if (ammoSystem.GetAmount(ammoType) <= 0) {
                HandleShootSfx(false);
                HandleClickSfx();
            }
        } else {
            HandleShootSfx(false);
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        ammoSystem.Consume(ammoType);
        muzzleFlash.Play();
        HandleShootSfx(true);
        RaycastHit hit; 
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
            Debug.Log(hit.transform.name);
            CreateHitImpactVfx(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) {
                target.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void CreateHitImpactVfx(RaycastHit hit)
    {
        GameObject futureDestroy = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        futureDestroy.transform.parent = vfxParent.transform;
        Destroy(futureDestroy, 1f);
    }

    private void HandleShootSfx(bool isShooting) {
        if (isShooting && !shootSfx.isPlaying) {
            shootSfx.Play();
        } else if (!isShooting && shootSfx.isPlaying && shootSfx.loop == true) {
            shootSfx.Stop();
        }
    }
    private void HandleClickSfx()
    {
        if (clickSfx && !clickSfx.isPlaying) {
            clickSfx.Play();
        }
    }

    public void SetImageAlpha(float alpha) {
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}
