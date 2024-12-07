using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // ������ �������
    public Transform player;         // ������ �� ������
    public float spawnDistance = 20f; // ��������� �� ������, ��� ���������� ������
    public AudioClip scareSound;     // �������� ����
    public float monsterLifetime = 5f; // �����, ����� ������� ������ ��������
    public float lookSpeed = 2f;     // ��������, � ������� ������ �������������� �� �������

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        // �������� ��������� ����� � 20 �������� �� X ��� Z �� ������
        Vector3 spawnPosition = player.position + new Vector3(
            Random.Range(-spawnDistance, spawnDistance),
            0,
            Random.Range(-spawnDistance, spawnDistance)
        );

        // ������� �������
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        monster.transform.LookAt(player); // ������ �������������� � ������

        // ����������� ����
        AudioSource audioSource = monster.AddComponent<AudioSource>();
        audioSource.clip = scareSound;
        audioSource.Play();

        // �������� ������� ������� ������ �� �������
        StartCoroutine(LookAtMonster(monster.transform));

        // ���������� ������� ����� �����
        Destroy(monster, monsterLifetime);
    }

    private IEnumerator LookAtMonster(Transform monster)
    {
        Camera playerCamera = Camera.main;

        while (true)
        {
            // ������ ������������ ������ � �������
            Vector3 direction = (monster.position - playerCamera.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerCamera.transform.rotation = Quaternion.Slerp(
                playerCamera.transform.rotation,
                targetRotation,
                Time.deltaTime * lookSpeed
            );

            // ������� �� �����, ���� ������ ��� ����� ������� �� �������
            if (Quaternion.Angle(playerCamera.transform.rotation, targetRotation) < 1f)
                break;

            yield return null;
        }
    }
}
