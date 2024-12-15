using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public Transform player;       // ������ �� ������
    public Transform playerCamera; // ������ �� ������ ������
    public float speed = 3f;       // �������� �������� �������
    public float stopDistance = 1.5f; // ����������, �� ������� ������ ���������������

    private bool playerLooking = false;

    void Update()
    {
        // ���������, ������� �� ����� �� �������
        Vector3 directionToMonster = (transform.position - playerCamera.position).normalized;
        float angle = Vector3.Angle(playerCamera.forward, directionToMonster);

        playerLooking = angle < 45f; // ���� ���� ������ 45�, ����� ������� �� �������

        if (!playerLooking)
        {
            // ������� ������� � ������, ���� �� �� �������
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            // ������� ������� � ������� ������
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // ������������ ������� ����� � ������
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}
