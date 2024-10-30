using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioMixer gameAudioMixer; // ���������� ���� ��� Audio Mixer
    public Slider VolumeSlider; // ��� ������� ���������

    void Start()
    {
        // ���������� ��������� �������� �������� �� ����������� ������ ��� �� ��������� 0.5
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        VolumeSlider.value = savedVolume;

        // ��������� ��������� ���������
        SetVolume(savedVolume);

        // �������� �� ��������� �������� ��������
        VolumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // ����������� ��������� � �������� ��� Audio Mixer
        float volumeInDecibels = Mathf.Log10(volume) * 20;
        gameAudioMixer.SetFloat("MasterVolume", volumeInDecibels);

        // ��������� �������� ���������
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
}
