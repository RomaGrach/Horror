using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Префаб монстра
    public Camera playerCamera;      // Ссылка на камеру игрока
    public float spawnDistance = 20f; // Дистанция от камеры, где появляется монстр
    public AudioClip scareSound;     // Страшный звук
    public float monsterLifetime = 5f; // Время, через которое монстр исчезнет
    public float lookSpeed = 2f;     // Скорость, с которой камера поворачивается на монстра

    private bool triggered = false; // Флаг, чтобы событие произошло один раз

    private void OnTriggerEnter(Collider other)
    {
        // Если событие уже сработало или объект не камера, ничего не делаем
        if (triggered || other.gameObject != playerCamera.gameObject)
            return;

        triggered = true; // Устанавливаем, что триггер активирован
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        // Определяем случайную точку на расстоянии от камеры
        Vector3 spawnPosition = playerCamera.transform.position + new Vector3(
            Random.Range(-spawnDistance, spawnDistance),
            0,
            Random.Range(-spawnDistance, spawnDistance)
        );

        // Спавним монстра
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        monster.transform.LookAt(playerCamera.transform.position); // Монстр поворачивается к камере

        // Проигрываем звук
        AudioSource audioSource = monster.AddComponent<AudioSource>();
        audioSource.clip = scareSound;
        audioSource.Play();

        // Начинаем плавный поворот камеры на монстра
        StartCoroutine(LookAtMonster(monster.transform));

        // Уничтожаем монстра через заданное время
        Destroy(monster, monsterLifetime);
    }

    private IEnumerator LookAtMonster(Transform monster)
    {
        // Получаем главную камеру игрока
        Transform cameraTransform = playerCamera.transform;

        while (true)
        {
            // Плавно поворачиваем камеру к монстру
            Vector3 direction = (monster.position - cameraTransform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            cameraTransform.rotation = Quaternion.Slerp(
                cameraTransform.rotation,
                targetRotation,
                Time.deltaTime * lookSpeed
            );

            // Останавливаем, если угол поворота стал очень маленьким
            if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1f)
                break;

            yield return null;
        }
    }
}
