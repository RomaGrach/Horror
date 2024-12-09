using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterStat : MonoBehaviour
{
    public Transform player;       // ������ �� ������
    public float speed = 2f;       // �������� �������� �������

    private bool playerLookingAtMonster = false;

    void Update()
    {
        // ���������, ������� �� ����� �� �������
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(player.forward, directionToPlayer);

        // ���� ���� ������ 45 ��������, �������, ��� ����� ������� �� �������
        playerLookingAtMonster = angle < 45f;

        if (!playerLookingAtMonster)
        {
            // ���� ����� �� �������, ������ �������� � ������
            Vector3 direction = directionToPlayer.normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}

