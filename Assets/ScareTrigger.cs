using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScareTrigger2 : MonoBehaviour
{
    public GameObject monster;         // ������ �� ������ �������
    public Transform startPosition;    // �����, ������ ������ ����� ��������
    public Transform endPosition;      // �����, ��� ������ �������� ��������
    public float runSpeed = 5f;        // �������� �������
    public AudioSource scareSound;     // ����, ������� ������������� ��� ������������ ��������
    private bool hasTriggered = false; // ����, ����� ������� ���������� ���� ���

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) // ���������, ���������� �� �����
        {
            hasTriggered = true;

            // �������� ����
            if (scareSound != null)
            {
                scareSound.Play();
            }

            // ��������� �������
            StartCoroutine(MoveMonster());
        }
    }

    private System.Collections.IEnumerator MoveMonster()
    {
        // ������������ ������� � ���������� ��������� �������
        monster.SetActive(true);
        monster.transform.position = startPosition.position;

        // ������� ������� � �������� �����
        while (Vector3.Distance(monster.transform.position, endPosition.position) > 0.1f)
        {
            monster.transform.position = Vector3.MoveTowards(
                monster.transform.position,
                endPosition.position,
                runSpeed * Time.deltaTime
            );
            yield return null; // ��� ���� ����
        }

        // ������ ������� ����� ���������� ��������
        monster.SetActive(false);
    }
}

