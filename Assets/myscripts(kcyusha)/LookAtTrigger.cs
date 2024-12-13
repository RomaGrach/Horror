using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LookAtTrigger : MonoBehaviour
{
    public Transform playerCamera;  // ������ �� ������ ������
    public AudioSource audioSource; // ������ �� ��������� AudioSource
    public float detectionAngle = 30f; // ����, ��� ������� ����������� �����������
    public float audioVolume = 2.0f;  // ����������� ��������� ����� (1.0 = 100%)
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
            audioSource.volume = audioVolume; // ������������� ���������
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

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Disappear()
    {
        // ������ ������ ����������
        gameObject.SetActive(false);
    }
}
