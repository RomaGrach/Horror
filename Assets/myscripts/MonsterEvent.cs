using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;  // ������ �������
    public Camera playerCamera;       // ������ ������
    public float spawnDistance = 10f; // ���������� ��� ������ �������
    public AudioClip scareSound;      // �������� ����
    public float monsterLifetime = 5f; // �����, ����� ������� ������ ��������
    public float lookSpeed = 2f;      // ��������, � ������� ������ ��������������

    private bool triggered = false;   // ����, ����� ������� ���������� ���� ���

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���-�� ����� � �������: " + other.gameObject.name);

        if (!triggered && other.gameObject == playerCamera.gameObject)
        {
            Debug.Log("������ ����� � �������!");
            triggered = true;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        Debug.Log("SpawnMonster ������!");

        // 1. ������������ ������� ����� �������
        Vector3 forward = playerCamera.transform.forward.normalized; // ������ ����������� ������
        Vector3 spawnPosition = playerCamera.transform.position * spawnDistance;

        // 2. ������������� ������ ������� �� ������ ������
        spawnPosition.y = playerCamera.transform.position.y;

        // 3. ������ �������
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("������ ��������� �� �������: " + spawnPosition);

        // 4. ������������ ������� ����� � ������
        monster.transform.LookAt(playerCamera.transform);

        // 5. ����������� ����
        AudioSource audioSource = monster.AddComponent<AudioSource>();
        audioSource.clip = scareSound;
        audioSource.Play();

        // 6. ������������ ������ �� �������
        StartCoroutine(LookAtMonster(monster.transform));

        // 7. ���������� ������� ����� �������� �����
        Destroy(monster, monsterLifetime);
    }


    private IEnumerator LookAtMonster(Transform monsterTransform)
    {
        Transform cameraTransform = playerCamera.transform;
        FirstPersonLook c_s = playerCamera.gameObject.GetComponent<FirstPersonLook>();
        c_s.enabled= false;

        while (monsterTransform != null) // ���������, ���������� �� ������
        {
            // ������������ ����������� �� �������
            Vector3 direction = (monsterTransform.position - cameraTransform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // ������ ������������ ������
            cameraTransform.rotation = Quaternion.Slerp(
                cameraTransform.rotation,
                targetRotation,
                Time.deltaTime * lookSpeed
            );

            // �������������, ���� ���� ����� ������� � ������� ��������� ������ 1 �������
            if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1f)
            {
                c_s.enabled = true;
                break;
            }
                

            yield return null; // ��� ��������� ����
        }
    }

}
