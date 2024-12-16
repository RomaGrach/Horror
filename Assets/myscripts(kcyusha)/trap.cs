using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class trap: MonoBehaviour
{
    public int damage = 20; // Урон, который наносит капкан
    public AudioSource trapSound; // Источник звука капкана
    public Animator trapAnimator; // Аниматор для анимации капкана
    public string trapAnimationTrigger = "Activate"; // Триггер анимации капкана

    private bool isTriggered = false; // Флаг для предотвращения повторного срабатывания

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, наступил ли игрок на капкан
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true; // Устанавливаем флаг, чтобы капкан не срабатывал повторно

            // Наносим урон игроку (если у него есть скрипт, обрабатывающий здоровье)
           /* PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
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
}

