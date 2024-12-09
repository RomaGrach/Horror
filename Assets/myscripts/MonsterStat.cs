using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public Transform player;       // Ссылка на игрока
    public Transform playerCamera; // Ссылка на камеру игрока
    public float speed = 3f;       // Скорость движения монстра
    public float stopDistance = 1.5f; // Расстояние, на котором монстр останавливается

    private bool playerLooking = false;

    void Update()
    {
        // Проверяем, смотрит ли игрок на монстра
        Vector3 directionToMonster = (transform.position - playerCamera.position).normalized;
        float angle = Vector3.Angle(playerCamera.forward, directionToMonster);

        playerLooking = angle < 45f; // Если угол меньше 45°, игрок смотрит на монстра

        if (!playerLooking)
        {
            // Двигаем монстра к игроку, если он не смотрит
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            // Двигаем монстра в сторону игрока
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Поворачиваем монстра лицом к игроку
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}
