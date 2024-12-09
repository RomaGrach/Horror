using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterStat : MonoBehaviour
{
    public Transform player;       // —сылка на игрока
    public float speed = 2f;       // —корость движени€ монстра

    private bool playerLookingAtMonster = false;

    void Update()
    {
        // ѕровер€ем, смотрит ли игрок на монстра
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(player.forward, directionToPlayer);

        // ≈сли угол меньше 45 градусов, считаем, что игрок смотрит на монстра
        playerLookingAtMonster = angle < 45f;

        if (!playerLookingAtMonster)
        {
            // ≈сли игрок не смотрит, монстр движетс€ к игроку
            Vector3 direction = directionToPlayer.normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}

