using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{

    [SerializeField] GameObject rightGate;
    [SerializeField] GameObject leftGate;
    [SerializeField] float animTime = 0.5f;
    [SerializeField] int keysNeeded = 2;
    [SerializeField] float interactDistance = 5f;
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject keysText;
    GameObject player;
    Inventory inventory;
    ObjectivesManager objectivesManager;
    AudioSource gateSfx;

    void Start() {
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        objectivesManager = FindObjectOfType<ObjectivesManager>();
        ShowInteractText(false);
        ShowKeysText(false);
        gateSfx = GetComponent<AudioSource>();
    }

    void Update() {
        HandleInteract();
    }

    private void HandleInteract()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= interactDistance) {
            ShowInteractText(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                OpenGate();
            }
        } else {
            ShowInteractText(false);
            ShowKeysText(false);
        }
    }

    private void ShowInteractText(bool showInteractText)
    {
        interactText.SetActive(showInteractText);
    }

    private void ShowKeysText(bool showKeysText) {
        
        keysText.SetActive(showKeysText);
    }

    public void OpenGate() {
        if (inventory.Keys < 2) {
            int remainingKeys = keysNeeded - inventory.Keys;
            string message = $"Requires {remainingKeys} more {(remainingKeys == 1 ? "key" : "keys")} to open.";
            keysText.GetComponent<TextMeshPro>().text = message;
            ShowKeysText(true);
            objectivesManager.AddObjective(Objectives.gate);
            return;
        }
        rightGate.transform.Rotate(new Vector3(0, 1.3f, 0), 55);
        leftGate.transform.Rotate(new Vector3(0, -1.3f, 0), 55f);   
        ShowInteractText(false);
        ShowKeysText(false);
        AudioSource.PlayClipAtPoint(gateSfx.clip, transform.position);
        objectivesManager.CompleteObjective(Objectives.gate);
        objectivesManager.CompleteObjective(Objectives.crypt);
        objectivesManager.AddObjective(Objectives.ritual);
    }
}
