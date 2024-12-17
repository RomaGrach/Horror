using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class trap: MonoBehaviour
{
    public int damage = 20; // ����, ������� ������� ������
    public AudioSource trapSound; // �������� ����� �������
    public Animator trapAnimator; // �������� ��� �������� �������
    public string trapAnimationTrigger = "Activate"; // ������� �������� �������

    private bool isTriggered = false; // ���� ��� �������������� ���������� ������������

    void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ����� �� ������
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true; // ������������� ����, ����� ������ �� ���������� ��������

            // ������� ���� ������ (���� � ���� ���� ������, �������������� ��������)
           /* PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }*/

            // ������������� ���� �������
            if (trapSound != null)
            {
                trapSound.Play();
            }

            // ��������� �������� �������
            if (trapAnimator != null)
            {
                trapAnimator.SetTrigger(trapAnimationTrigger);
            }

            // �������������: ����� �������������� ������ ����� ������������
            // Destroy(gameObject, 5f); // ������� ������ ����� 5 ������
        }
    }
}

