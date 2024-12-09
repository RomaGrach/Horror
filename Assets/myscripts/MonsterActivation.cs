using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterActivation : MonoBehaviour

{
    public GameObject monster; // Ссылка на объект монстра
    public Transform player;   // Ссылка на объект игрока
    public AudioClip scareSound; // Звук появления
    private AudioSource audioSource;
    public float followDistance = 5f; // Расстояние, на которое монстр подходит к игроку
    public float disappearDistance = 15f; // Расстояние, на котором монстр исчезает
    public float angleThreshold = 160f; // Угол, при котором монстр начинает двигаться

    private bool isMonsterActive = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (monster != null)
        {
            monster.SetActive(false); // Изначально монстр выключен
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateMonster();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DeactivateMonster();
        }
    }

    void Update()
    {
        if (isMonsterActive && monster != null && player != null)
        {
            Vector3 directionToPlayer = (player.position - monster.transform.position).normalized;
            float angleToPlayer = Vector3.Angle(directionToPlayer, player.forward);

            // Если игрок отвернулся (угол > 160)
            if (angleToPlayer > angleThreshold)
            {
                float distance = Vector3.Distance(player.position, monster.transform.position);

                if (distance > followDistance)
                {
                    // Приближаем монстра к игроку
                    Vector3 targetPosition = player.position - directionToPlayer * followDistance;
                    monster.transform.position = Vector3.Lerp(monster.transform.position, targetPosition, Time.deltaTime);
                }
            }

            // Проверка на исчезновение
            float playerDistance = Vector3.Distance(player.position, monster.transform.position);
            if (playerDistance > disappearDistance)
            {
                DeactivateMonster();
            }
        }
    }

    void ActivateMonster()
    {
        if (monster != null)
        {
            monster.SetActive(true);
            isMonsterActive = true;

            // Воспроизвести звук
            if (scareSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(scareSound);
            }
        }
    }

    void DeactivateMonster()
    {
        if (monster != null)
        {
            monster.SetActive(false);
            isMonsterActive = false;
        }
    }
}
