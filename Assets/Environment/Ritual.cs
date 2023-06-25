using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class Ritual : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] float interactDistance = 10f;
    [SerializeField] float portalFadeDuration = 2.0f;
    [SerializeField] Canvas victoryCanvas;
    [SerializeField] TextMeshPro closeText;
    Transform player;
    FirstPersonController playerInputController;
    AudioSource ritualSfx;
    bool isPortalClosed = false;

    void Start() {
        player = FindObjectOfType<PlayerHealth>().transform;
        playerInputController = player.GetComponent<FirstPersonController>();
        victoryCanvas.enabled = false;
        closeText.enabled = false;
        ritualSfx = GetComponent<AudioSource>();
        AudioSource.PlayClipAtPoint(ritualSfx.clip, transform.position);
    }

    void Update()
    {
        if (boss.GetComponent<EnemyHealth>().IsDead && !isPortalClosed){
            if (Vector3.Distance(transform.position, player.position) < interactDistance) {
                closeText.enabled = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    StartCoroutine(ClosePortal());
                }
            }
        }
    }

    private IEnumerator ClosePortal()
    {
        closeText.enabled = false;
        foreach (ParticleSystem part in GetComponentsInChildren<ParticleSystem>()) {
            part.Stop();
        }
        isPortalClosed = true;
        yield return new WaitForSeconds(portalFadeDuration);
        victoryCanvas.enabled = true;
        Time.timeScale = 0;
        playerInputController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
