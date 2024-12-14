using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spiderai : MonoBehaviour
{
    public Transform pointA; // Точка A
    public Transform pointB; // Точка B
    public float moveSpeed = 2f; // Скорость передвижения паука
    public AudioClip moveSound; // Звук движения паука
    public AudioClip attackSound; // Звук атаки паука
    public float attackRange = 2f; // Радиус атаки паука
    private Animator animator; // Аниматор для управления анимациями
    private Transform targetPoint; // Текущая цель движения
    private AudioSource audioSource; // Компонент для воспроизведения звуков
    private bool isAttacking = false; // Флаг атаки

    void Start()
    {
        // Устанавливаем начальную цель движения
        targetPoint = pointA;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (audioSource != null && moveSound != null)
        {
            audioSource.clip = moveSound;
            audioSource.loop = true; // Звук движения повторяется
            audioSource.Play();
        }
    }

    void Update()
    {
        if (!isAttacking)
        {
            MoveBetweenPoints();
        }
    }

    void MoveBetweenPoints()
    {
        // Двигаем паука к целевой точке
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Если паук достиг цели, меняем направление
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackPlayer(other.transform);
        }
    }

    void AttackPlayer(Transform player)
    {
        isAttacking = true; // Останавливаем движение паука
        audioSource.Stop(); // Прекращаем звук движения
        animator.SetTrigger("spidertrig");
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound); // Проигрываем звук атаки
        }

        // Направляем паука к игроку
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * (moveSpeed * 1.5f) * Time.deltaTime;

        // Здесь можно добавить нанесение урона или другие эффекты
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false; // Возвращаем паука к его маршруту
            targetPoint = Vector3.Distance(transform.position, pointA.position) < Vector3.Distance(transform.position, pointB.position)
                ? pointA
                : pointB;

            if (audioSource != null && moveSound != null)
            {
                audioSource.clip = moveSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}

