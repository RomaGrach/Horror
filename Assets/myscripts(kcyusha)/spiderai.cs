using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class spiderai : MonoBehaviour
{
    public Transform pointA; // ����� A
    public Transform pointB; // ����� B
    public float moveSpeed = 2f; // �������� ������������ �����
    public AudioClip moveSound; // ���� �������� �����
    public AudioClip attackSound; // ���� ����� �����
    public float attackRange = 2f; // ������ ����� �����
    private Animator animator; // �������� ��� ���������� ����������
    private Transform targetPoint; // ������� ���� ��������
    private AudioSource audioSource; // ��������� ��� ��������������� ������
    private bool isAttacking = false; // ���� �����

    void Start()
    {
        // ������������� ��������� ���� ��������
        targetPoint = pointA;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (audioSource != null && moveSound != null)
        {
            audioSource.clip = moveSound;
            audioSource.loop = true; // ���� �������� �����������
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
        // ������� ����� � ������� �����
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // ���� ���� ������ ����, ������ �����������
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
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Если компонент PlayerHealth найден, вызываем метод TakeDamage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
        }
    }

    void AttackPlayer(Transform player)
    {
        isAttacking = true; // ������������� �������� �����
        audioSource.Stop(); // ���������� ���� ��������
        animator.SetTrigger("spidertrig");
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound); // ����������� ���� �����
        }


        // ���������� ����� � ������
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * (moveSpeed * 1.5f) * Time.deltaTime;

        // ����� ����� �������� ��������� ����� ��� ������ �������
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false; // ���������� ����� � ��� ��������
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

