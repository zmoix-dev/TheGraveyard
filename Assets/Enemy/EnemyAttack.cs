using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    [SerializeField] AudioSource hitPlayerSfx;
    PlayerHealth target;

    void Start() {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent() {
        if (target == null) {
            return;
        } else {
            target.takeDamage(damage, DamageType.PHYSICAL);
            if (hitPlayerSfx && !hitPlayerSfx.isPlaying) {
                hitPlayerSfx.Play();
            }
        }
    }
}
