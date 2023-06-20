using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool canShoot = true;

    GameObject vfxParent;

    void Start() {
        vfxParent = GameObject.FindWithTag("VfxParent");
    }

    // Update is called once per frame
    void Update()
    {
        HandleFire();
    }

    private void HandleFire()
    {
        if (Input.GetMouseButton(0)) {
            if (ammoSystem.GetAmount(ammoType) > 0 && canShoot) {
                 StartCoroutine("Shoot");
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        ammoSystem.Consume(ammoType);
        muzzleFlash.Play();
        RaycastHit hit; 
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
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
}
