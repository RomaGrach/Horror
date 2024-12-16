using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSpawn : MonoBehaviour
{
    public GameObject monster; // Объект монстра
    public AudioSource scareSound; // Источник звука
    public float monsterDuration = 5f; // Время, в течение которого монстр виден
    public float fadeDuration = 1f; // Длительность затухания звука
    public float spawnDistance = 2f; // Расстояние, на котором монстр появляется перед игроком
    public float blinkInterval = 0.2f; // Интервал между мельканиями монстра

    private bool hasActivated = false; // Флаг для предотвращения повторной активации

    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что игрок вошел в триггер
        if (other.CompareTag("Player") && !hasActivated)
        {
            hasActivated = true; // Убедимся, что событие срабатывает только один раз
            StartCoroutine(SpawnMonster(other.transform));
        }
    }

    private IEnumerator SpawnMonster(Transform player)
    {
        // Рассчитываем позицию перед игроком
        Vector3 spawnPosition = player.position + player.forward * spawnDistance;

        // Устанавливаем монстра на рассчитанную позицию
        monster.transform.position = spawnPosition;

        // Поворачиваем монстра лицом к игроку
        monster.transform.LookAt(player);

        // Воспроизводим звук
        scareSound.Play();

        // Запускаем мелькание монстра
        float elapsedTime = 0f;
        Renderer monsterRenderer = monster.GetComponent<Renderer>();

        while (elapsedTime < monsterDuration)
        {
            // Переключаем видимость монстра
            monsterRenderer.enabled = !monsterRenderer.enabled;

            // Ждём интервал
            yield return new WaitForSeconds(blinkInterval);

            // Увеличиваем прошедшее время
            elapsedTime += blinkInterval;
        }

        // Убедимся, что монстр исчезает окончательно
        monsterRenderer.enabled = false;

        // Плавно затухаем звук и затем останавливаем его
        yield return StartCoroutine(FadeOutSound(scareSound, fadeDuration));
    }

    private IEnumerator FadeOutSound(AudioSource audioSource, float fadeDuration)
    {
        // Сохраняем начальную громкость звука
        float startVolume = audioSource.volume;

        // Постепенно уменьшаем громкость до 0
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Останавливаем звук
        audioSource.Stop();

        // Восстанавливаем громкость для будущего использования
        audioSource.volume = startVolume;
    }
}

