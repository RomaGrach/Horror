using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
public class LookAtTrigger : MonoBehaviour
{
    public Transform playerCamera;  // ������ �� ������ ������
    public AudioSource audioSource; // ������ �� ��������� AudioSource
    public float detectionAngle = 30f; // ����, ��� ������� ����������� �����������
    private bool hasPlayedSound = false;
    void Update()
    {
        // ������������ ������ ����������� �� ������ � �������
        Vector3 directionToModel = transform.position - playerCamera.position;

        // ��������� ���� ����� ������������ ������� ������ � ������������ �� ������
        float angle = Vector3.Angle(playerCamera.forward, directionToModel);

        if (angle <= detectionAngle && !hasPlayedSound)
        {
            // ���� ����� ������� �� ������ � ���� ��� �� ��� �������������
            audioSource.Play();
            hasPlayedSound = true;

            // ����������� ������������ ������ �� 4 �������
            Invoke("Disappear", 2.5f);
        }
    }
    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = GameObject.Find("First Person Camera").transform;
        }
    }
    void Disappear()
    {
        // ������ ������ ����������
        gameObject.SetActive(false);
    }
}
