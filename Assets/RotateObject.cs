using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 initialRotation; // Изначальный угол поворота
    public Vector3 fromAngle; // Угол начала вращения
    public Vector3 toAngle; // Угол конца вращения
    public float rotationSpeed = 1f; // Скорость вращения

    private bool isRotating = false;

    void Start()
    {
        // Поворачиваем объект на заданный угол при старте
        transform.eulerAngles = initialRotation;
    }

    void Update()
    {
        if (isRotating)
        {
            // Вращаем объект от текущего угла до toAngle с заданной скоростью
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.Euler(toAngle),
                rotationSpeed * Time.deltaTime
            );

            // Проверяем, достиг ли объект конечного угла
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(toAngle)) < 0.1f)
            {
                isRotating = false;
                Debug.Log("Rotation complete");
            }
        }
    }

    // Функция для начала вращения объекта
    public void StartRotation()
    {
        isRotating = true;
        // Устанавливаем начальный угол перед началом вращения
        transform.eulerAngles = fromAngle;
    }
}
