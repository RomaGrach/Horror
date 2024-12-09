using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterActivation : MonoBehaviour

{
    public GameObject monster; // ������ �� ������ �������
    public Transform player;   // ������ �� ������ ������
    public AudioClip scareSound; // ���� ���������
    private AudioSource audioSource;
    public float followDistance = 5f; // ����������, �� ������� ������ �������� � ������
    public float disappearDistance = 15f; // ����������, �� ������� ������ ��������
    public float angleThreshold = 160f; // ����, ��� ������� ������ �������� ���������

    private bool isMonsterActive = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (monster != null)
        {
            monster.SetActive(false); // ���������� ������ ��������
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateMonster();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeactivateMonster();
        }
    }

    void Update()
    {
        if (isMonsterActive && monster != null && player != null)
        {
            Vector3 directionToPlayer = (player.position - monster.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(directionToPlayer, player.forward);

            // ���� ����� ���������� (���� > 160)
            if (angleToPlayer > angleThreshold)
            {
                float distance = Vector3.Distance(player.position, monster.transform.position);

                if (distance > followDistance)
                {
                    // ���������� ������� � ������
                    Vector3 targetPosition = player.position - directionToPlayer * followDistance;
                    monster.transform.position = Vector3.Lerp(monster.transform.position, targetPosition, Time.deltaTime);
                }
            }

            // �������� �� ������������
            float playerDistance = Vector3.Distance(player.position, monster.transform.position);
            if (playerDistance > disappearDistance)
            {
                DeactivateMonster();
            }
        }
    }

    void ActivateMonster()
    {
        if (monster != null)
        {
            monster.SetActive(true);
            isMonsterActive = true;

            // ������������� ����
            if (scareSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(scareSound);
            }
        }
    }

    void DeactivateMonster()
    {
        if (monster != null)
        {
            monster.SetActive(false);
            isMonsterActive = false;
        }
    }
}
