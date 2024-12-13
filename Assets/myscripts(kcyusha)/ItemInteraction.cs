using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInteraction : MonoBehaviour
{
    public GameObject choicePanel; // ������ �� ������ � �������
    private bool isPlayerNearby = false; // �������� �� �������� ������

    void Update()
    {
        // ���� ����� ����� � ����� ������ �������������� (��������, "E")
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowChoicePanel();
        }
    }

    private void ShowChoicePanel()
    {
        // ���������� ������ ������
        choicePanel.SetActive(true);
        Time.timeScale = 0f; // ������ ���� �� �����
    }

    public void EatItem()
    {
        // �������� "������"
        Debug.Log("�� ����� �������!");
        CloseChoicePanel();
    }

    public void LeaveItem()
    {
        // �������� "��������"
        Debug.Log("�� �������� �������!");
        CloseChoicePanel();
    }

    private void CloseChoicePanel()
    {
        // ��������� ������ ������
        choicePanel.SetActive(false);
        Time.timeScale = 1f; // ������������ ����
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� ����� �����
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ����� ����
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}

