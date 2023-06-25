using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticleAttack : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    float t;
    public void CreateAttackParticles() {
        t = Time.time;
        Debug.Log($"Particles! {t}");

        if (particles == null) {
            return;
        } else {
            particles.Play();
        }
    }
}
