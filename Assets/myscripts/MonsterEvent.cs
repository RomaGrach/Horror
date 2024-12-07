using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Префаб монстра
    public Transform player;         // Ссылка на игрока
    public float spawnDistance = 20f; // Дистанция от игрока, где появляется монстр
    public AudioClip scareSound;     // Страшный звук
    public float monsterLifetime = 5f; // Время, через которое монстр исчезнет
    public float lookSpeed = 2f;     // Скорость, с которой камера поворачивается на монстра

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        // Выбираем случайную точку в 20 единицах по X или Z от игрока
        Vector3 spawnPosition = player.position + new Vector3(
            Random.Range(-spawnDistance, spawnDistance),
            0,
            Random.Range(-spawnDistance, spawnDistance)
        );

        // Спавним монстра
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        monster.transform.LookAt(player); // Монстр поворачивается к игроку

        // Проигрываем звук
        AudioSource audioSource = monster.AddComponent<AudioSource>();
        audioSource.clip = scareSound;
        audioSource.Play();

        // Начинаем плавный поворот камеры на монстра
        StartCoroutine(LookAtMonster(monster.transform));

        // Уничтожаем монстра через время
        Destroy(monster, monsterLifetime);
    }

    private IEnumerator LookAtMonster(Transform monster)
    {
        Camera playerCamera = Camera.main;

        while (true)
        {
            // Плавно поворачиваем камеру к монстру
            Vector3 direction = (monster.position - playerCamera.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerCamera.transform.rotation = Quaternion.Slerp(
                playerCamera.transform.rotation,
                targetRotation,
                Time.deltaTime * lookSpeed
            );

            // Выходим из цикла, если камера уже почти смотрит на монстра
            if (Quaternion.Angle(playerCamera.transform.rotation, targetRotation) < 1f)
                break;

            yield return null;
        }
    }
}
