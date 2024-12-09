using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActivation : MonoBehaviour
{
    public GameObject monster; // Объект монстра
    public Transform player; // Игрок
    public AudioClip spawnSound; // Звук при появлении
    private AudioSource audioSource; // Источник звука
    public float detectDistance = 10f; // Максимальное расстояние для обнаружения игрока
    public float moveSpeed = 3f; // Скорость монстра
    public float lookAngleThreshold = 160f; // Порог угла для того, чтобы монстр двигался
    private bool isMonsterActive = false; // Флаг, активен ли монстр

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        monster.SetActive(false); // Монстр не активен в начале
    }

    void Update()
    {
        if (isMonsterActive)
        {
            // Проверка угла между направлением взгляда камеры игрока и направлением к монстру
            Vector3 directionToMonster = monster.transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToMonster.normalized);

            // Если угол больше порогового значения, монстр начинает двигаться
            if (angle > lookAngleThreshold)
            {
                MoveMonsterTowardsPlayer();
            }
            else
            {
                // Монстр останавливается, если игрок его видит
                StopMonster();
            }
        }
    }

    void MoveMonsterTowardsPlayer()
    {
        // Проверка расстояния до игрока
        float distanceToPlayer = Vector3.Distance(monster.transform.position, player.position);

        if (distanceToPlayer > detectDistance)
        {
            // Если игрок далеко, монстр останавливается
            StopMonster();
        }
        else
        {
            // Если игрок в пределах видимости, монстр приближается
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void StopMonster()
    {
        // Логика для остановки монстра или скрытия
        monster.SetActive(false);
        isMonsterActive = false;
    }

    // Включить монстра при попадании в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что это игрок
        {
            monster.SetActive(true); // Активируем монстра
            audioSource.PlayOneShot(spawnSound); // Воспроизводим звук появления
            isMonsterActive = true; // Устанавливаем флаг активности монстра
        }
    }

    // Деактивировать монстра, если игрок покидает область
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что это игрок
        {
            StopMonster();
        }
    }
}
