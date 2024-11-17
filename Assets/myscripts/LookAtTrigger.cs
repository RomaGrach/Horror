using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
public class LookAtTrigger : MonoBehaviour
{
    public Transform playerCamera;  // Ссылка на камеру игрока
    public AudioSource audioSource; // Ссылка на компонент AudioSource
    public float detectionAngle = 30f; // Угол, при котором срабатывает обнаружение
    private bool hasPlayedSound = false;
    void Update()
    {
        // Рассчитываем вектор направления от игрока к объекту
        Vector3 directionToModel = transform.position - playerCamera.position;

        // Проверяем угол между направлением взгляда камеры и направлением на модель
        float angle = Vector3.Angle(playerCamera.forward, directionToModel);

        if (angle <= detectionAngle && !hasPlayedSound)
        {
            // Если игрок смотрит на модель и звук ещё не был воспроизведен
            audioSource.Play();
            hasPlayedSound = true;

            // Откладываем исчезновение модели на 4 секунды
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
        // Делаем модель неактивной
        gameObject.SetActive(false);
    }
}
