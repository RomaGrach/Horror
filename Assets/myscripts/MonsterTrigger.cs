using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterTrigger : MonoBehaviour
{
    public GameObject monster;       // ������ �� ������ �������
    public Transform playerCamera;  // ������ �� ������ ������
    public float randomIntervalMin = 5f; // ����������� ����� �� ���������� ���������
    public float randomIntervalMax = 15f; // ������������ ����� �� ���������� ���������
    public float monsterDistance = 5f;   // ���������� ����� �������

    private AudioSource monsterSound;  // �������� ����� �������

    private void Start()
    {
        // ��������, ��� ������ �������
        if (playerCamera == null)
        {
            GameObject cameraObject = GameObject.Find("First Person Camera");
            if (cameraObject != null)
            {
                playerCamera = cameraObject.transform;
            }
            else
            {
                Debug.LogError("������ � ������ 'First Person Camera' �� �������!");
                return;
            }
        }

        // ��������� ������� ������� �������
        if (monster == null)
        {
            Debug.LogError("������ �� �������� � �������!");
            return;
        }

        // �������� AudioSource �������
        monsterSound = monster.GetComponent<AudioSource>();
        if (monsterSound == null)
        {
            Debug.LogError("�� ������� ������� ����������� ��������� AudioSource!");
            return;
        }

        // ��������� ���� ��������� ��������� �������
        StartCoroutine(TriggerMonsterAtRandom());
    }

    private IEnumerator TriggerMonsterAtRandom()
    {
        while (true)
        {
            // ��� ��������� �����
            yield return new WaitForSeconds(Random.Range(randomIntervalMin, randomIntervalMax));

            // ���������� �������
            ShowMonster();
        }
    }

    private void ShowMonster()
    {
        Debug.Log("���������� �������!");
        monster.transform.position = playerCamera.position + playerCamera.forward * 5f;
        monster.transform.rotation = Quaternion.LookRotation(playerCamera.forward);
        monster.SetActive(true);

        // ������������ ������� ����� � ������
        monster.transform.rotation = Quaternion.LookRotation(playerCamera.forward);

        // ���������� �������
        monster.SetActive(true);

        // ����������� ����
        if (monsterSound != null)
        {
            monsterSound.Play();
        }

        // �������� ������� ����� �������� �����
        StartCoroutine(HideMonster());
    }

    private IEnumerator HideMonster()
    {
        yield return new WaitForSeconds(0.5f); // �����, ���� ������ ������� �������
        monster.SetActive(false);
    }
}
