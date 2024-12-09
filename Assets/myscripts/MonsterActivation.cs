using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActivation : MonoBehaviour
{
    public GameObject monster; // Ссылка на модель монстра
    public Transform player;   // Ссылка на игрока
    public float disappearDistance = 20f; // Расстояние, на котором монстр исчезает

    private bool isPlayerInZone = false;
    private MonsterDisappearance monsterDisappearance;

    void Start()
    {
        // Получаем компонент MonsterDisappearance у монстра
        monsterDisappearance = monster.GetComponent<MonsterDisappearance>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверка, игрок ли зашел в зону
        {
            isPlayerInZone = true;
            monster.SetActive(true); // Активируем монстра
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Игрок вышел из зоны
        {
            isPlayerInZone = false;
            // Вызываем плавное исчезновение
            if (monsterDisappearance != null)
            {
                monsterDisappearance.FadeOut();
            }
            else
            {
                monster.SetActive(false); // Если компонента нет, просто отключаем
            }
        }
    }

    void Update()
    {
        if (isPlayerInZone)
        {
            float distance = Vector3.Distance(player.position, monster.transform.position);

            // Если игрок отошел слишком далеко, вызываем исчезновение
            if (distance > disappearDistance)
            {
                if (monsterDisappearance != null)
                {
                    monsterDisappearance.FadeOut();
                }
                else
                {
                    monster.SetActive(false);
                }
            }
        }
    }
}


