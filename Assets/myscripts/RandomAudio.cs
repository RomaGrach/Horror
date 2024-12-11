using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour

{
    public AudioClip[] audioClips; // Массив аудиоклипов
    public AudioSource audioSource; // Аудиоисточник
    public float minPause = 1f; // Минимальная пауза между звуками
    public float maxPause = 3f; // Максимальная пауза между звуками
    public float minDistance = 1f; // Минимальное расстояние для громкости 100%
    public float maxDistance = 5f; // Максимальное расстояние для полного затухания звука

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioClips.Length > 0 && audioSource != null)
        {
            ConfigureAudioSource();
            StartCoroutine(PlaySoundsRandomly());
        }
        else
        {
            Debug.LogError("Не заданы AudioClips или отсутствует AudioSource!");
        }
    }

    private void ConfigureAudioSource()
    {
        audioSource.spatialBlend = 1.0f; // Включаем 3D-звук
        audioSource.rolloffMode = AudioRolloffMode.Linear; // Линейное затухание громкости
        audioSource.minDistance = minDistance; // Радиус полной громкости
        audioSource.maxDistance = maxDistance; // Радиус полного затухания
    }

    private IEnumerator PlaySoundsRandomly()
    {
        while (true)
        {
            // Выбираем случайный звук из массива
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip clip = audioClips[randomIndex];

            // Проигрываем звук
            audioSource.clip = clip;
            audioSource.Play();

            // Ждем окончания звука + случайную паузу
            yield return new WaitForSeconds(clip.length + Random.Range(minPause, maxPause));
        }
    }
}
