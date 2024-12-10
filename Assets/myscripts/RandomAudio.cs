using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public AudioClip[] audioClips; // ������ �����������
    public AudioSource audioSource; // �������������
    public float minPause = 1f; // ����������� ����� ����� �������
    public float maxPause = 3f; // ������������ ����� ����� �������

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioClips.Length > 0 && audioSource != null)
        {
            StartCoroutine(PlaySoundsRandomly());
        }
        else
        {
            Debug.LogError("�� ������ AudioClips ��� ����������� AudioSource!");
        }
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