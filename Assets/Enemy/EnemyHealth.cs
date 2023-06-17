using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    float currentHitPoints;

    void Start() {
        currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(float damage) {
        BroadcastMessage("OnDamageTaken");
        currentHitPoints -= damage;
        if (currentHitPoints <= 0) {
            Destroy(gameObject);
        }
    }
}
