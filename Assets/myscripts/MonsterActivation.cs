using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActivation : MonoBehaviour
{
    public GameObject monster;         // ������ �� ������ �������
    public Transform player;           // ������ �� ������
    public Transform playerCamera;     // ������ �� ������ ������
    public float appearDistance = 5f;  // ��������� ��������� ������� ����� �������
    public float disappearDistance = 20f; // ���������, �� ������� ������ ��������
    public AudioSource audioSource;    // �������� ����� ��� ���������������

    private bool monsterActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ���� ����� ������� � �������
        {
            ActivateMonster();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ���� ����� ������� �� ��������
        {
            DeactivateMonster();
        }
    }

    void Update()
    {
        if (monsterActive)
        {
            float distance = Vector3.Distance(player.position, monster.transform.position);

            // ������������ �������, ���� ����� ������
            if (distance > disappearDistance)
            {
                DeactivateMonster();
            }
        }
    }

    void ActivateMonster()
    {
        if (!monsterActive)
        {
            // ����������� ������� ������
            Vector3 forwardDirection = playerCamera.forward;
            forwardDirection.y = 0; // ������� ������������ ������������, ����� ������ �� �����
            forwardDirection.Normalize();

            // ������� ��������� ������� ����� �������
            Vector3 spawnPosition = player.position + forwardDirection * appearDistance;
            monster.transform.position = spawnPosition;

            // ������������ ������� ����� � ������
            monster.transform.rotation = Quaternion.LookRotation(-forwardDirection);

            // ���������� �������
            monster.SetActive(true);
            monsterActive = true;

            // ����������� ���� ���������
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }


    void DeactivateMonster()
    {
        if (monsterActive)
        {
            // ������������ �������
            monster.SetActive(false);
            monsterActive = false;
        }
    }
}
