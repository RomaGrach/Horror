using UnityEngine;

public class KiznosDrawer : MonoBehaviour
{
    // Переменная для задания радиуса кизноса
    public float kizmas = 5.0f;

    // Цвет кизноса
    public Color kiznosColor = Color.red;

    // Метод для отрисовки кизноса
    void OnDrawGizmos()
    {
        // Устанавливаем цвет кизноса
        Gizmos.color = kiznosColor;

        // Рисуем сферу вокруг объекта с заданным радиусом
        Gizmos.DrawWireSphere(transform.position, kizmas);
    }
}