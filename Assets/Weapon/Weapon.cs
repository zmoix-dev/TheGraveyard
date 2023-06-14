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
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range)) {
            Debug.Log($"Hit this: {hit.transform.name}");
            CreateHitImpactVfx(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null) {
                target.TakeDamage(damage);
            }
        }
        
    }

    private void CreateHitImpactVfx(RaycastHit hit)
    {
        GameObject futureDestroy = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        futureDestroy.transform.parent = vfxParent.transform;
        Destroy(futureDestroy, 1f);
    }
}
