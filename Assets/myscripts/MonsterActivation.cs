using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActivation : MonoBehaviour
{
    public GameObject monster;         // Ссылка на объект монстра
    public Transform player;           // Ссылка на игрока
    public Transform playerCamera;     // Ссылка на камеру игрока
    public float appearDistance = 5f;  // Дистанция появления монстра перед игроком
    public float disappearDistance = 20f; // Дистанция, на которой монстр исчезает
    public AudioSource audioSource;    // Источник звука для воспроизведения

    private bool monsterActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Если игрок заходит в триггер
        {
            ActivateMonster();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Если игрок выходит из триггера
        {
            DeactivateMonster();
        }
    }

    void Update()
    {
        if (monsterActive)
        {
            float distance = Vector3.Distance(player.position, monster.transform.position);

            // Деактивируем монстра, если игрок далеко
            if (distance > disappearDistance)
            {
                DeactivateMonster();
            }
        }
    }

    void ActivateMonster()
    {
        if (!monsterActive)
        {
            // Направление взгляда камеры
            Vector3 forwardDirection = playerCamera.forward;
            forwardDirection.y = 0; // Убираем вертикальную составляющую, чтобы монстр не летал
            forwardDirection.Normalize();

            // Позиция появления монстра перед игроком
            Vector3 spawnPosition = player.position + forwardDirection * appearDistance;
            monster.transform.position = spawnPosition;

            // Поворачиваем монстра лицом к игроку
            monster.transform.rotation = Quaternion.LookRotation(-forwardDirection);

            // Активируем монстра
            monster.SetActive(true);
            monsterActive = true;

            // Проигрываем звук появления
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }


    void DeactivateMonster()
    {
        if (monsterActive)
        {
            // Деактивируем монстра
            monster.SetActive(false);
            monsterActive = false;
        }
    }
}
