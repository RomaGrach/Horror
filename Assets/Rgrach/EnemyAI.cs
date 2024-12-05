using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float detectionDistance = 10f; // Дистанция обнаружения игрока
    public float detectionAngle = 60f; // Угол обнаружения игрока
    public float attackDistance = 2f; // Дистанция атаки
    public float losePlayerDistance = 15f; // Дистанция, после которой враг теряет игрока
    public Transform[] waypoints; // Точки маршрута для патрулирования
    public float patrolSpeed = 2f; // Скорость патрулирования
    public float chaseSpeed = 4f; // Скорость погони

    private NavMeshAgent agent; // Компонент NavMeshAgent для перемещения
    private Animator animator; // Аниматор для управления анимациями
    public bool playerDetected = false; // Флаг обнаружения игрока
    private int currentWaypointIndex = 0; // Индекс текущей точки маршрута

    // Переменные для анимации
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int JumpAttack = Animator.StringToHash("JumpAttack");
    private static readonly int Roar = Animator.StringToHash("Roar");
    private static readonly int SlowRun = Animator.StringToHash("SlowRun");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Go = Animator.StringToHash("Go");

    private void Start()
    {
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
