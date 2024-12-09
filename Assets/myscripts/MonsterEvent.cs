using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;  // Префаб монстра
    public Camera playerCamera;       // Камера игрока
    public float spawnDistance = 10f; // Расстояние для спавна монстра
    public AudioClip scareSound;      // Страшный звук
    public float monsterLifetime = 5f; // Время, через которое монстр исчезнет
    public float lookSpeed = 2f;      // Скорость, с которой камера поворачивается

    private bool triggered = false;   // Флаг, чтобы триггер срабатывал один раз

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Что-то вошло в триггер: " + other.gameObject.name);

        if (!triggered && other.gameObject == playerCamera.gameObject)
        {
            Debug.Log("Камера вошла в триггер!");
            triggered = true;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        Debug.Log("SpawnMonster вызван!");

        // 1. Рассчитываем позицию перед камерой
        Vector3 forward = playerCamera.transform.forward.normalized; // Вектор направления камеры
        Vector3 spawnPosition = playerCamera.transform.position + forward * spawnDistance;

        // 2. Устанавливаем высоту монстра на уровне камеры
        spawnPosition.y = playerCamera.transform.position.y;

        // 3. Создаём монстра
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Монстр заспавнен на позиции: " + spawnPosition);

        // 4. Поворачиваем монстра лицом к камере
        monster.transform.LookAt(playerCamera.transform);

        // 5. Проигрываем звук
        AudioSource audioSource = monster.AddComponent<AudioSource>();
        audioSource.clip = scareSound;
        audioSource.Play();

        // 6. Поворачиваем камеру на монстра
        StartCoroutine(LookAtMonster(monster.transform));

        // 7. Уничтожаем монстра через заданное время
        Destroy(monster, monsterLifetime);
    }


    private IEnumerator LookAtMonster(Transform monsterTransform)
    {
        Transform cameraTransform = playerCamera.transform;

        while (monsterTransform != null) // Проверяем, существует ли монстр
        {
            // Рассчитываем направление на монстра
            Vector3 direction = (monsterTransform.position - cameraTransform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Плавно поворачиваем камеру
            cameraTransform.rotation = Quaternion.Slerp(
                cameraTransform.rotation,
                targetRotation,
                Time.deltaTime * lookSpeed
            );

            // Останавливаем, если угол между текущим и целевым поворотом меньше 1 градуса
            if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 1f)
                break;

            yield return null; // Ждём следующий кадр
        }
    }

}
