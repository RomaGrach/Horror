using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform; // Трансформ камеры
    public float shakeDuration = 1.0f; // Продолжительность тряски
    public float shakeAmount = 0.1f; // Начальная амплитуда тряски
    public float increaseRate = 0.01f; // Скорость увеличения тряски
    public float decreaseFactor = 1.0f; // Фактор уменьшения тряски

    private Vector3 originalPos;
    private float currentShakeAmount;

    void Start()
    {
        // Сохраняем начальную позицию камеры
        originalPos = cameraTransform.localPosition;
        currentShakeAmount = shakeAmount;
    }

    void Update()
    {
        // Если время тряски еще не вышло
        if (shakeDuration > 0)
        {
            // Тряска камеры с увеличивающейся амплитудой
            cameraTransform.localPosition = originalPos + Random.insideUnitSphere * currentShakeAmount;

            // Уменьшаем оставшееся время тряски
            shakeDuration -= Time.deltaTime * decreaseFactor;

            // Увеличиваем амплитуду тряски
            currentShakeAmount += increaseRate * Time.deltaTime;
        }
        else
        {
            // Возвращаем камеру в начальную позицию
            shakeDuration = 0;
            cameraTransform.localPosition = originalPos;
        }
    }

    // Метод для начала тряски
    public void StartShake(float duration, float initialAmount, float increaseRate)
    {
        shakeDuration = duration;
        shakeAmount = initialAmount;
        this.increaseRate = increaseRate;
        currentShakeAmount = shakeAmount;
    }
}
