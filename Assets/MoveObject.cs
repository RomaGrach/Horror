using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float initialHeight = 40f; // Высота, на которую нужно поднять объект изначально
    public float belowSurfaceHeight = 5f; // Высота, на которую нужно опустить объект ниже поверхности
    public LayerMask groundLayer; // Слой "Ground"

    void Start()
    {
        // Поднятие объекта на заданную высоту
        transform.position += Vector3.up * initialHeight;

        // Проверка высоты вниз до поверхности, учитывая только слой "Ground"
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Сдвиг объекта к поверхности
            transform.position = hit.point;

            // Опускание объекта ниже поверхности на заданную высоту
            transform.position -= Vector3.up * belowSurfaceHeight;
        }
        else
        {
            Debug.LogError("Поверхность не найдена под объектом на слое 'Ground'.");
        }
    }
}
