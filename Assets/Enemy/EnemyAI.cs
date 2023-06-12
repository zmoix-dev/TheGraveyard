using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float aggroRadius = 10f;
    NavMeshAgent navMeshAgent;
    Vector3 spawnPosition;
    bool isEngaged;
    Transform engagedWith;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        HandleShouldEngage();
        HandleEngage();
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }

    private void HandleShouldEngage()
    {
        if (IsTargetInRange(aggroRadius)) {
            isEngaged = true;
            engagedWith = target;
        } else {
            isEngaged = false;
            engagedWith = null;
        }
    }

    private bool IsTargetInRange(float range)
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);
        return range >= targetDistance;
    }

    private void HandleEngage() {
        if (isEngaged && engagedWith) {
            navMeshAgent.SetDestination(engagedWith.position);
            if (IsTargetInRange(navMeshAgent.stoppingDistance)) {
                HandleAttack();
            }
        } else {
            navMeshAgent.SetDestination(spawnPosition);
        }
    }

    private void HandleAttack()
    {
        Debug.Log("Attack!");
    }
}
