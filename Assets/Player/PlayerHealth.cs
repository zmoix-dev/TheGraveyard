using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float startingHealth = 100f;
    [SerializeField] Canvas damageTakenCanvas;
    [SerializeField] Canvas magicDamageTakenCanvas;
    [SerializeField] float fadeTime = 0.5f;
    [SerializeField] Canvas healthDisplayCanvas;
    float currentHealth;

    void Start() {
        currentHealth = startingHealth;
        damageTakenCanvas.enabled = false;
        magicDamageTakenCanvas.enabled = false;
    }

    void OnParticleCollision(GameObject other) {
        if (other.GetComponent<ParticleDamage>()) {
            takeDamage(other.GetComponent<ParticleDamage>().Damage, DamageType.MAGICAL);    
        }
    }

    public void takeDamage(float damage, DamageType type) {
        currentHealth -= damage;
        UpdateHealthBar();
        StopAllCoroutines();
        StartCoroutine(DisplayDamageCanvas(type));
        if (currentHealth <= 0) {
            HandlePlayerDeath();
        }
    }

    private void UpdateHealthBar()
    {
        healthDisplayCanvas.GetComponentInChildren<Slider>().value = currentHealth / startingHealth;
    }

    private IEnumerator DisplayDamageCanvas(DamageType type)
    {
        Image img;
        if (type.Equals(DamageType.MAGICAL)) {
            img = magicDamageTakenCanvas.GetComponentInChildren<Image>();
            magicDamageTakenCanvas.enabled = true;
        } else {
            img = damageTakenCanvas.GetComponentInChildren<Image>();
            damageTakenCanvas.enabled = true;
        }
        
        float elapsedTime = 0.0f;
        Color c = img.color;
        while (elapsedTime < fadeTime) {
            yield return new YieldInstruction();
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            img.color = c;
        }
        
        magicDamageTakenCanvas.enabled = false;
        damageTakenCanvas.enabled = false;
        c.a = 1.0f;
        img.color = c;
    }
    
    void HandlePlayerDeath() {
        GetComponent<PlayerDeathHandler>().HandleDeath();
    }
}
