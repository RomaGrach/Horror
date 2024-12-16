using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект, вошедший в триггер, - это игрок
        if (other.CompareTag("Player"))
        {
            // Воспроизводим музыку
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Останавливаем музыку, когда игрок выходит из триггера
     
    }
}
