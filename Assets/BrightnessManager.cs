using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Slider brightnessSlider;
    private Light sceneLight;

    void Start()
    {
        // ����� Directional Light � �����
        sceneLight = FindObjectOfType<Light>();

        // ��������� ��������� �������� ������� ��������
        if (sceneLight != null)
        {
            brightnessSlider.value = sceneLight.intensity;
        }

        // ����������� �� ��������� �������� ��������
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

    public void ChangeBrightness(float intensity)
    {
        if (sceneLight != null)
        {
            sceneLight.intensity = intensity;
        }
    }
}

