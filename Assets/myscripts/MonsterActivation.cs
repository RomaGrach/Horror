using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActivation : MonoBehaviour
{
    public GameObject monster; // ������ �� ������ �������
    public Transform player;   // ������ �� ������
    public float disappearDistance = 20f; // ����������, �� ������� ������ ��������

    private bool isPlayerInZone = false;
    private MonsterDisappearance monsterDisappearance;

    void Start()
    {
        // �������� ��������� MonsterDisappearance � �������
        monsterDisappearance = monster.GetComponent<MonsterDisappearance>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ��������, ����� �� ����� � ����
        {
            isPlayerInZone = true;
            monster.SetActive(true); // ���������� �������
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ����� ����� �� ����
        {
            isPlayerInZone = false;
            // �������� ������� ������������
            if (monsterDisappearance != null)
            {
                monsterDisappearance.FadeOut();
            }
            else
            {
                monster.SetActive(false); // ���� ���������� ���, ������ ���������
            }
        }
    }

    void Update()
    {
        if (isPlayerInZone)
        {
            float distance = Vector3.Distance(player.position, monster.transform.position);

            // ���� ����� ������ ������� ������, �������� ������������
            if (distance > disappearDistance)
            {
                if (monsterDisappearance != null)
                {
                    monsterDisappearance.FadeOut();
                }
                else
                {
                    monster.SetActive(false);
                }
            }
        }
    }
}


