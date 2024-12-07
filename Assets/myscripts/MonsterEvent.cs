using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterEvent : MonoBehaviour
{
    public GameObject monster; // ������ �� ������ �������
    public Transform monsterPosition; // �������, ��� ���������� ������
    public float lookSpeed = 2f; // �������� �������� ������
    public float monsterDisappearDelay = 3f; // ����� �� ������������ �������
    public AudioClip tensionSound; // ���������� ����

    private bool eventTriggered = false; // ����� ������� ����������� ������ ���� ���
    private AudioSource audioSource; // �������� �����
    private Transform playerCamera; // ������ ������

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ���� ������ �� �����
        GameObject cameraObject = GameObject.Find("First Person Camera");
        if (cameraObject != null)
        {
            playerCamera = cameraObject.transform;
        }
        else
        {
            Debug.LogError("������ 'First Person Camera' �� �������!");
        }

        if (monster != null)
        {
            monster.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (eventTriggered) return;

        // ���� ����� ������ � �������
        if (other.CompareTag("Player"))
        {
            eventTriggered = true;
            StartCoroutine(MonsterSequence());
        }
    }

    private IEnumerator MonsterSequence()
    {
        // �������� �������
        if (monster != null)
        {
            monster.SetActive(true);
            monster.transform.position = monsterPosition.position;
        }

        // ������������� ���������� ����
        if (audioSource != null && tensionSound != null)
        {
            audioSource.clip = tensionSound;
            audioSource.Play();
        }

        // ������������ ������ �� �������
        Vector3 directionToMonster = (monsterPosition.position - playerCamera.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToMonster);

        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime * lookSpeed;
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, time);
            yield return null;
        }

        // �������� ����� ������������� �������
        yield return new WaitForSeconds(monsterDisappearDelay);

        // ��������� �������
        if (monster != null)
        {
            monster.SetActive(false);
        }
    }
}

