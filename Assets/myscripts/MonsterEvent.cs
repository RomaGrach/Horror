using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // ������ �������
    public Camera playerCamera;      // ������ �� ������ ������
    public float spawnDistance = 20f; // ��������� �� ������, ��� ���������� ������
    public AudioClip scareSound;     // �������� ����
    public float monsterLifetime = 5f; // �����, ����� ������� ������ ��������
    public float lookSpeed = 2f;     // ��������, � ������� ������ �������������� �� �������

    private bool triggered = false; // ����, ����� ������� ��������� ���� ���

    private void OnTriggerEnter(Collider other)
    {
        // ���� ������� ��� ��������� ��� ������ �� ������, ������ �� ������
        if (triggered || other.gameObject != playerCamera.gameObject)
            return;

        triggered = true; // �������������, ��� ������� �����������
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        // ���������� ��������� ����� �� ���������� �� ������
        Vector3 spawnPosition = playerCamera.transform.position + new Vector3(
            Random.Range(-spawnDistance, spawnDistance),
            0,
            Random.Range(-spawnDistance, spawnDistance)
        );

        // ������� �������
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        monster.transform.LookAt(playerCamera.transform.position); // ������ �������������� � ������

        // ����������� ����
        AudioSource audioSource = monster.AddComponent<AudioSource>();
        audioSource.clip = scareSound;
        audioSource.Play();

        // �������� ������� ������� ������ �� �������
        StartCoroutine(LookAtMonster(monster.transform));

        // ���������� ������� ����� �������� �����
        Destroy(monster, monsterLifetime);
    }

    private IEnumerator LookAtMonster(Transform monster)
    {
        // �������� ������� ������ ������
        Transform cameraTransform = playerCamera.transform;

        while (true)
        {
            // ������ ������������ ������ � �������
            Vector3 direction = (monster.position - cameraTransform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            cameraTransform.rotation = Quaternion.Slerp(
                cameraTransform.rotation,
                targetRotation,
                Time.deltaTime * lookSpeed
            );

            // �������������, ���� ���� �������� ���� ����� ���������
            if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1f)
                break;

            yield return null;
        }
    }
}
