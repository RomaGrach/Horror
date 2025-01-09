using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Присвойте сюда объект игрока через инспектор
    public Transform player;

    // Присвойте сюда объект изображения на Canvas
    public RectTransform minimapIcon;

    // Множитель для преобразования координат
    public float scale = 0.001f;

    void Update()
    {
        // Берем позицию игрока
        Vector3 playerPosition = player.position;

        // Преобразуем позицию игрока для миникарты
        Vector2 minimapPosition = new Vector2(playerPosition.x * scale, playerPosition.z * scale);

        // Применяем преобразованную позицию к иконке на миникарте
        minimapIcon.anchoredPosition = minimapPosition;
    }
}
