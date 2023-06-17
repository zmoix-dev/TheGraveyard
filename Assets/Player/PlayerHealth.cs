using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float startingHealth = 100f;
    float currentHealth;

    void Start() {
        currentHealth = startingHealth;
    }

    public void takeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            HandlePlayerDeath();
        }
    }

    void HandlePlayerDeath() {
        GetComponent<PlayerDeathHandler>().HandleDeath();
    }
}
