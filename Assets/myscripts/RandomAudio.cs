using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour

{
    public AudioClip[] audioClips; // ������ �����������
    public AudioSource audioSource; // �������������
    public float minPause = 1f; // ����������� ����� ����� �������
    public float maxPause = 3f; // ������������ ����� ����� �������
    public float minDistance = 1f; // ����������� ���������� ��� ��������� 100%
    public float maxDistance = 5f; // ������������ ���������� ��� ������� ��������� �����

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioClips.Length > 0 && audioSource != null)
        {
            ConfigureAudioSource();
            StartCoroutine(PlaySoundsRandomly());
        }
        else
        {
            Debug.LogError("�� ������ AudioClips ��� ����������� AudioSource!");
        }
    }

    private void ConfigureAudioSource()
    {
        audioSource.spatialBlend = 1.0f; // �������� 3D-����
        audioSource.rolloffMode = AudioRolloffMode.Linear; // �������� ��������� ���������
        audioSource.minDistance = minDistance; // ������ ������ ���������
        audioSource.maxDistance = maxDistance; // ������ ������� ���������
    }

    private IEnumerator PlaySoundsRandomly()
    {
        while (true)
        {
            // �������� ��������� ���� �� �������
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip clip = audioClips[randomIndex];

            // ����������� ����
            audioSource.clip = clip;
            audioSource.Play();

            // ���� ��������� ����� + ��������� �����
            yield return new WaitForSeconds(clip.length + Random.Range(minPause, maxPause));
        }
    }
}
