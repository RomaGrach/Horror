using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class LookAtTrigger : MonoBehaviour
{
    public Transform playerCamera;  // Ссылка на камеру игрока
    public AudioSource audioSource; // Ссылка на компонент AudioSource
    public float detectionAngle = 30f; // Угол, при котором срабатывает обнаружение
    public float audioVolume = 2.0f;  // Регулировка громкости звука (1.0 = 100%)
    public float activationDistance = 10f; // Расстояние до игрока для активации
    private bool hasPlayedSound = false;

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

    void Update()
    {
        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, playerCamera.position);

        if (distanceToPlayer <= activationDistance && !hasPlayedSound)
        {
            // Рассчитываем вектор направления от игрока к объекту
            Vector3 directionToModel = transform.position - playerCamera.position;

            // Проверяем угол между направлением взгляда камеры и направлением на модель
            float angle = Vector3.Angle(playerCamera.forward, directionToModel);

            if (angle <= detectionAngle)
            {
                // Если игрок смотрит на модель и звук ещё не был воспроизведён
                audioSource.volume = audioVolume; // Устанавливаем громкость
                audioSource.Play();
                hasPlayedSound = true;

                // Откладываем исчезновение модели на 2.5 секунды
                Invoke("Disappear", 2.5f);
            }
        }
    }

    void Disappear()
    {
        // Делаем модель неактивной
        gameObject.SetActive(false);
    }
}
