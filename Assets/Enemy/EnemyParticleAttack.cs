using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    EnemyHealth health;

    void Start() {
        health = GetComponent<EnemyHealth>();
    }

    public void CreateAttackParticles() {
        if (health.IsDead || particles == null) {
            return;
        } else {
            particles.Play();
        }
    }
}
