using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float aggroRadius = 10f;
    [SerializeField] float turnSpeed = 5f;
    Transform target;
    NavMeshAgent navMeshAgent;
    Vector3 spawnPosition;
    bool isEngaged;
    float attackRange = 2.5f;


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

    private void HandleShouldEngage(){
        if (IsTargetInRange(aggroRadius)) {
            isEngaged = true;
        } 
    }

    private bool IsTargetInRange(float range) {
        float targetDistance = Vector3.Distance(target.position, transform.position);
        return range >= targetDistance;
    }

    private void HandleEngage() {
        if (isEngaged) {
            HandleChase();
            if (IsTargetInRange(navMeshAgent.stoppingDistance)) {
                HandleAttack();
            } else {
                HandleStopAttack();
            }
        } else {
            //HandleReset();
        }
    }

    private void HandleAttack() {
        FaceTarget();
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void HandleStopAttack() {
        GetComponent<Animator>().SetBool("Attack", false);
    }


    private void HandleChase() {
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.stoppingDistance = attackRange;
        navMeshAgent.SetDestination(target.position);
    }

    private void HandleReset() {
        Animator animator = GetComponent<Animator>();
        if (IsAtLocation(spawnPosition)) {
            animator.SetTrigger("Idle");
        } else {
            animator.SetTrigger("Move");
            navMeshAgent.stoppingDistance = 0f;
            navMeshAgent.SetDestination(spawnPosition);
        }
    }

    private bool IsAtLocation(Vector3 location){
        return transform.position.x == location.x && transform.position.z == location.z;
    }

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken() {
        isEngaged = true;
    }
}
