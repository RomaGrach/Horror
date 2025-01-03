using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 20; // Урон, который наносит капкан
    public AudioSource trapSound; // Источник звука капкана
    public Animator trapAnimator; // Аниматор для анимации капкана
    public string trapAnimationTrigger = "Activate"; // Триггер анимации капкана
    public float activationDistance = 1f; // Расстояние для активации

    private bool isTriggered = false; // Флаг для предотвращения повторного срабатывания
    private Transform playerTransform; // Ссылка на трансформ игрока

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Игрок не найден! Убедитесь, что объект игрока имеет тег 'Player'.");
        }
    }

    void Update()
    {
        // Проверяем расстояние до игрока
        if (!isTriggered && playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= activationDistance)
            {
                ActivateTrap();
            }
        }
    }

    private void ActivateTrap()
    {
        isTriggered = true; // Устанавливаем флаг, чтобы капкан не срабатывал повторно

        // Наносим урон игроку (если у него есть скрипт, обрабатывающий здоровье)
        /* PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }*/

        // Воспроизводим звук капкана
        if (trapSound != null)
        {
            trapSound.Play();
        }

        // Запускаем анимацию капкана
        if (trapAnimator != null)
        {
            trapAnimator.SetTrigger(trapAnimationTrigger);
        }

        // Дополнительно: можно деактивировать капкан после срабатывания
        // Destroy(gameObject, 5f); // Удалить капкан через 5 секунд
    }
}
