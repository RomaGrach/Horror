using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioMixer gameAudioMixer; // Перетащите сюда ваш Audio Mixer
    public Slider VolumeSlider; // Ваш слайдер громкости

    void Start()
    {
        // Установите начальное значение слайдера из сохраненных данных или по умолчанию 0.5
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        VolumeSlider.value = savedVolume;

        // Примените начальную громкость
        SetVolume(savedVolume);

        // Подписка на изменение значения слайдера
        VolumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Преобразуем громкость в децибелы для Audio Mixer
        float volumeInDecibels = Mathf.Log10(volume) * 20;
        gameAudioMixer.SetFloat("MasterVolume", volumeInDecibels);

        // Сохраняем значение громкости
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
