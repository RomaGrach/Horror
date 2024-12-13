using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class MonsterMovement : MonoBehaviour
{
    public Transform player; // ������ �� ������ ������
    public float detectionDistance = 10f; // ��������� ����������� ������
    public float detectionAngle = 60f; // ���� ����������� ������
    public float attackDistance = 2f; // ��������� �����
    public float losePlayerDistance = 15f; // ���������, ����� ������� ���� ������ ������
    public Transform[] waypoints; // ����� �������� ��� ��������������
    public float patrolSpeed = 2f; // �������� ��������������
    public float chaseSpeed = 4f; // �������� ������

    private NavMeshAgent agent; // ��������� NavMeshAgent ��� �����������
    private Animator animator; // �������� ��� ���������� ����������
    public bool playerDetected = false; // ���� ����������� ������
    private int currentWaypointIndex = 0; // ������ ������� ����� ��������

    public float knockbackForce = 10f; // ���� ������������
    public float knockbackUpwardForce = 2f; // ���� ������������ �����
    public bool waitGround = false;

    public float timeAtakNow = 0f;
    public float timeAtak = 2f;
    public bool atacAVALIBALE = false;

    // ���������� ��� ��������
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int JumpAttack = Animator.StringToHash("JumpAttack");
    private static readonly int Roar = Animator.StringToHash("Roar");
    private static readonly int SlowRun = Animator.StringToHash("SlowRun");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Go = Animator.StringToHash("Go");

    public GroundCheck groundCheck;
    public FirstPersonMovement s_g;


    public event System.Action Jumped;

    private void Start()
    {
        timeAtakNow = timeAtak;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToNextWaypoint();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (!playerDetected && distanceToPlayer <= detectionDistance && angleToPlayer <= detectionAngle / 2)
        {
            playerDetected = true;
            //animator.SetTrigger(Roar);
        }

        if (playerDetected)
        {
            if (distanceToPlayer <= attackDistance)
            {
                AttackPlayer();
            }
            else if (distanceToPlayer <= losePlayerDistance)
            {
                ChasePlayer();
            }
            else
            {
                playerDetected = false;
                GoToNextWaypoint();
            }
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
        /*
        if (waitGround && timeAtak > timeAtakNow)
        {
            if (groundCheck.isGrounded)
            {
                //.Log(groundCheck.isGrounded);
                s_g.enabled = true;
            }
        }
        */
        if (timeAtak > timeAtakNow)
        {
            timeAtakNow += Time.deltaTime;
        }



    }
    private void FixedUpdate()
    {
        if (waitGround && !(timeAtak > timeAtakNow))
        {
            if (groundCheck.isGrounded)
            {
                //Debug.Log(groundCheck.isGrounded);
                s_g.enabled = true;
            }
        }
    }






    void OnTriggerEnter(Collider other)
    {
        // ���� ������, �������� � �������, ����� ��� "Player"
        if (other.CompareTag("Player") && !(timeAtak > timeAtakNow))
        {
            Debug.Log("����");
            // �������� ��������� PlayerHealth � �������
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // ���� ��������� PlayerHealth ������, �������� ����� TakeDamage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }

            ApplyKnockback(other);
        }
    }

    private void ApplyKnockback(Collider player)
    {
        // �������� ��������� Rigidbody � ������
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        s_g = player.GetComponent<FirstPersonMovement>();
        groundCheck = player.GetComponentInChildren<GroundCheck>();

        // ���� ��������� Rigidbody ������, ��������� ���� ������������
        if (playerRigidbody != null)
        {
            Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
            knockbackDirection.y = 0; // ������� ������������ ������������
            Vector3 knockback = knockbackDirection * knockbackForce + Vector3.up * knockbackUpwardForce;
            Jumped?.Invoke();
            s_g.enabled = false;
            //playerRigidbody.AddForce(transform.forward * 100 * knockbackForce, ForceMode.Impulse);
            playerRigidbody.AddForce(transform.up * 100 * knockbackUpwardForce + transform.forward * 100 * knockbackForce);
            //playerRigidbody.AddForce(Vector3.up * 100 * 5);
            //playerRigidbody.AddForce(transform.up * 100 * 5 + transform.forward * 100 * 5);
            //s_g.enabled = true;
            waitGround = true;
            Jumped?.Invoke();

            timeAtakNow = 0f;
        }
    }


    private void AttackPlayer()
    {
        agent.isStopped = true;
        animator.SetTrigger(Attack);
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        animator.SetTrigger(Run);
    }

    private void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.isStopped = false;
        agent.speed = patrolSpeed;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        animator.SetTrigger(Go);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
