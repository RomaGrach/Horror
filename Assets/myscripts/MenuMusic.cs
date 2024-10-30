using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundClip; // Ваш звуковой файл
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundClip;
        audioSource.loop = true; // Звук будет повторяться
        audioSource.playOnAwake = true; // Звук начнет проигрываться при запуске игры
        audioSource.volume = 0.5f; // Установите громкость по вашему желанию

        audioSource.Play(); // Запускаем звук
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Не уничтожать объект при переходе на другую сцену

        if (FindObjectsOfType<BackgroundMusic>().Length > 1)
        {
            Destroy(gameObject); // Избегать дублирования объектов
        }
    }
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}
