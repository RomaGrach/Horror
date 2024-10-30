using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundClip; // ��� �������� ����
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundClip;
        audioSource.loop = true; // ���� ����� �����������
        audioSource.playOnAwake = true; // ���� ������ ������������� ��� ������� ����
        audioSource.volume = 0.5f; // ���������� ��������� �� ������ �������

        audioSource.Play(); // ��������� ����
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // �� ���������� ������ ��� �������� �� ������ �����

        if (FindObjectsOfType<BackgroundMusic>().Length > 1)
        {
            Destroy(gameObject); // �������� ������������ ��������
        }
    }
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
