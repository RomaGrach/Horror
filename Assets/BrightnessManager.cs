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
        // Найти Directional Light в сцене
        sceneLight = FindObjectOfType<Light>();

        // Присвоить начальное значение яркости слайдера
        if (sceneLight != null)
        {
            brightnessSlider.value = sceneLight.intensity;
        }

        // Подписаться на изменение значения слайдера
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

