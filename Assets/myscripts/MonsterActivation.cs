using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterActivation : MonoBehaviour
{
    public GameObject monster; // Объект монстра
    public Transform player; // Игрок
    public float detectDistance = 10f; // Максимальное расстояние для обнаружения игрока
    public float moveSpeed = 3f; // Скорость монстра
    public float lookAngleThreshold = 160f; // Порог угла для того, чтобы монстр двигался
    private bool isMonsterActive = false; // Флаг, активен ли монстр

    void Start()
    {
        monster.SetActive(false); // Монстр не активен в начале
    }

    void Update()
    {
        if (isMonsterActive)
        {
            Vector3 directionToMonster = monster.transform.position - player.position;
            float angle = Vector3.Angle(player.forward, directionToMonster.normalized);

            if (angle > lookAngleThreshold)
            {
                MoveMonsterTowardsPlayer();
            }
            else
            {
                StopMonster();
            }
        }
    }

    void MoveMonsterTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(monster.transform.position, player.position);

        if (distanceToPlayer > detectDistance)
        {
            StopMonster();
        }
        else
        {
            monster.transform.position = Vector3.MoveTowards(monster.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void StopMonster()
    {
        monster.SetActive(false);
        isMonsterActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.SetActive(true);
            isMonsterActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopMonster();
        }
    }
}
