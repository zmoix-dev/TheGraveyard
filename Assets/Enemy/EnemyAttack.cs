using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float damage = 20f;

    void Start() {
        target = GameObject.FindWithTag("Player");
    }

    public void AttackHitEvent() {
        Debug.Log("Attack noises.");
        if (target == null) {
            return;
        } else {
            target.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}
