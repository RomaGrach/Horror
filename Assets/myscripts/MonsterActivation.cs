using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterActivation : MonoBehaviour
{
    public GameObject monster; // ������ �������
    public Transform player; // �����
    public float detectDistance = 10f; // ������������ ���������� ��� ����������� ������
    public float moveSpeed = 3f; // �������� �������
    public float lookAngleThreshold = 160f; // ����� ���� ��� ����, ����� ������ ��������
    private bool isMonsterActive = false; // ����, ������� �� ������

    void Start()
    {
        monster.SetActive(false); // ������ �� ������� � ������
    }

    void Update()
    {
        if (isMonsterActive)
        {
            Vector3 directionToMonster = monster.transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToMonster.normalized);

            if (angle > lookAngleThreshold)
            {
                MoveMonsterTowardsPlayer();
            }
            else
            {
                StopMonster();
            }
        }
    }

    void MoveMonsterTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(monster.transform.position, player.position);

        if (distanceToPlayer > detectDistance)
        {
            StopMonster();
        }
        else
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void StopMonster()
    {
        monster.SetActive(false);
        isMonsterActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.SetActive(true);
            isMonsterActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopMonster();
        }
    }
}
