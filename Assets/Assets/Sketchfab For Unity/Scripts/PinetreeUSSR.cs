using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������, �������� � �������, - ��� �����
        if (other.CompareTag("Player"))
        {
            // ������������� ������
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ������������� ������, ����� ����� ������� �� ��������
     
    }
}
