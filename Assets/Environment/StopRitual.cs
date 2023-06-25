using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class StopRitual : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] float interactDistance = 10f;
    [SerializeField] float portalFadeDuration = 2.0f;
    [SerializeField] Canvas victoryCanvas;
    [SerializeField] TextMeshPro closeText;
    Transform player;
    FirstPersonController playerInputController;

    void Start() {
        player = FindObjectOfType<PlayerHealth>().transform;
        playerInputController = player.GetComponent<FirstPersonController>();
        victoryCanvas.enabled = false;
        closeText.enabled = false;
    }

    void Update()
    {
        if (boss.GetComponent<EnemyHealth>().IsDead){
            if (Vector3.Distance(transform.position, player.position) < interactDistance) {
                closeText.enabled = true;
                if (Input.GetKeyDown(KeyCode.E)) {
                    closeText.enabled = false;
                    StartCoroutine(ClosePortal());
                }
            }
        }
    }

    private IEnumerator ClosePortal()
    {
        foreach (ParticleSystem part in GetComponentsInChildren<ParticleSystem>()) {
            part.Stop();
        }
        yield return new WaitForSeconds(portalFadeDuration);
        victoryCanvas.enabled = true;
        
    }
}
