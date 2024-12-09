using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterActivation : MonoBehaviour
{
    public GameObject monster;          // ������ �� ������ �������
    public Transform playerCamera;      // ������ �� ������ ������
    public AudioClip scareSound;        // ���� ��� ��������� �������
    public float approachSpeed = 2f;    // �������� ����������� �������
    public float minDistance = 3f;      // ����������� ���������� �� ������
    public float disappearDistance = 10f; // ����������, �� ������� ������ ��������

    private AudioSource audioSource;
    private bool isMonsterActive = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = scareSound;

        // ���������, ��� ������ ���������� �����
        if (monster != null)
        {
            monster.SetActive(false);
        }
        else
        {
            Debug.LogError("������ �� �������� � ����������!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ������ �� ������ ������ (��� ��� ������) � �������
        if (other.CompareTag("Player"))
        {
            ActivateMonster();
        }
    }

    private void Update()
    {
        if (!isMonsterActive || monster == null || playerCamera == null) return;

        // ������������ ���� ����� ������������ ������ � ��������
        Vector3 directionToMonster = (monster.transform.position - playerCamera.parent.position).normalized;
        float angle = Vector3.Angle(playerCamera.parent.forward, directionToMonster);

        // ���� ����� ���������� (>160 ��������), ������ ������������
        if (angle > 160f)
        {
            float distance = Vector3.Distance(playerCamera.parent.position, monster.transform.position);
            if (distance > minDistance)
            {
                monster.transform.position = Vector3.MoveTowards(
                    monster.transform.position,
                    playerCamera.parent.position,
                    approachSpeed * Time.deltaTime
                );
            }
        }

        // ���� ����� ������ ������� ������, ������ ��������
        if (Vector3.Distance(playerCamera.position, monster.transform.position) > disappearDistance)
        {
            DeactivateMonster();
        }
    }

    private void ActivateMonster()
    {
        if (!isMonsterActive)
        {
            Debug.Log("������ �����������!");
            monster.SetActive(true);
            audioSource.Play();
            isMonsterActive = true;
            monster.transform.position = playerCamera.parent.transform.position + playerCamera.parent.transform.forward * 5;
        }
    }

    private void DeactivateMonster()
    {
        if (isMonsterActive)
        {
            Debug.Log("������ ��������!");
            monster.SetActive(false);
            isMonsterActive = false;
        }
    }
}
