using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 100f;
    float currentHitPoints;
    bool isDead = false;
    public bool IsDead { get { return isDead; }}

    void Start() {
        currentHitPoints = maxHitPoints;
    }

    public void TakeDamage(float damage) {
        if (isDead) {
            return;
        }
        BroadcastMessage("OnDamageTaken");
        currentHitPoints -= damage;
        if (currentHitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        GetComponent<Animator>().SetTrigger("Death");
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
