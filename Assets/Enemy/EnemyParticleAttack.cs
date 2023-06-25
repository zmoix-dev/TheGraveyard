using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] AudioSource sfx;
    EnemyHealth health;

    void Start() {
        health = GetComponent<EnemyHealth>();
    }

    public void CreateAttackParticles() {
        if (health.IsDead || particles == null) {
            return;
        } else {
            particles.Play();
            if (sfx && !sfx.isPlaying) {
                sfx.Play();
            }
        }
    }
}
