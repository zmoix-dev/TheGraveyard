using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float damage = 20f;

    public void AttackHitEvent() {
        if (target == null) {
            return;
        } else {
            Debug.Log("Attack noises.");
            target.GetComponent<PlayerHealth>().takeDamage(damage);
        }
    }
}
