using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawn : MonoBehaviour
{
    public GameObject monster; // ������ �������
    public AudioSource scareSound; // �������� �����
    public float monsterDuration = 5f; // �����, � ������� �������� ������ �����
    public float fadeDuration = 1f; // ������������ ��������� �����
    public float spawnDistance = 2f; // ����������, �� ������� ������ ���������� ����� �������
    public float blinkInterval = 0.2f; // �������� ����� ����������� �������

    private bool hasActivated = false; // ���� ��� �������������� ��������� ���������

    void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ����� ����� � �������
        if (other.CompareTag("Player") && !hasActivated)
        {
            hasActivated = true; // ��������, ��� ������� ����������� ������ ���� ���
            StartCoroutine(SpawnMonster(other.transform));
        }
    }

    private IEnumerator SpawnMonster(Transform player)
    {
        // ������������ ������� ����� �������
        Vector3 spawnPosition = player.position + player.forward * spawnDistance;

        // ������������� ������� �� ������������ �������
        monster.transform.position = spawnPosition;

        // ������������ ������� ����� � ������
        monster.transform.LookAt(player);

        // ������������� ����
        scareSound.Play();

        // ��������� ��������� �������
        float elapsedTime = 0f;
        Renderer monsterRenderer = monster.GetComponent<Renderer>();

        while (elapsedTime < monsterDuration)
        {
            // ����������� ��������� �������
            monsterRenderer.enabled = !monsterRenderer.enabled;

            // ��� ��������
            yield return new WaitForSeconds(blinkInterval);

            // ����������� ��������� �����
            elapsedTime += blinkInterval;
        }

        // ��������, ��� ������ �������� ������������
        monsterRenderer.enabled = false;

        // ������ �������� ���� � ����� ������������� ���
        yield return StartCoroutine(FadeOutSound(scareSound, fadeDuration));
    }

    private IEnumerator FadeOutSound(AudioSource audioSource, float fadeDuration)
    {
        // ��������� ��������� ��������� �����
        float startVolume = audioSource.volume;

        // ���������� ��������� ��������� �� 0
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // ������������� ����
        audioSource.Stop();

        // ��������������� ��������� ��� �������� �������������
        audioSource.volume = startVolume;
    }
}

