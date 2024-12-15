using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerActivateMonster : MonoBehaviour
{
    public GameObject monster; // ������ �������
    public AudioSource scareSound; // ������������� �� �������� ������
    public Transform player; // ������ �� ������ ������

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ������ �� ����� � ������� ��������
        if (other.CompareTag("Player"))
        {
            // ���������� ������ �������
            if (monster != null)
            {
                monster.SetActive(true);

                // �������� ������ � ������ �������� �������
                MonsterMovement movementScript = monster.GetComponent<MonsterMovement>();
                if (movementScript != null)
                {
                    //movementScript.target = player; // ������������� ������ ��� ����
                }
            }

            // ����������� �������� ����
            if (scareSound != null)
            {
                scareSound.Play();
            }
        }
    }
}
