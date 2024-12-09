using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterActivation : MonoBehaviour
{
    public GameObject monster;          // Ссылка на объект монстра
    public Transform playerCamera;      // Ссылка на камеру игрока
    public AudioClip scareSound;        // Звук для появления монстра
    public float approachSpeed = 2f;    // Скорость приближения монстра
    public float minDistance = 3f;      // Минимальное расстояние до игрока
    public float disappearDistance = 10f; // Расстояние, на котором монстр исчезнет

    private AudioSource audioSource;
    private bool isMonsterActive = false;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = scareSound;

        // Убедитесь, что монстр изначально скрыт
        if (monster != null)
        {
            monster.SetActive(false);
        }
        else
        {
            Debug.LogError("Монстр не привязан в инспекторе!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, входит ли объект игрока (или его камера) в триггер
        if (other.CompareTag("Player"))
        {
            ActivateMonster();
        }
    }

    private void Update()
    {
        if (!isMonsterActive || monster == null || playerCamera == null) return;

        // Рассчитываем угол между направлением камеры и монстром
        Vector3 directionToMonster = (monster.transform.position - playerCamera.parent.position).normalized;
        float angle = Vector3.Angle(playerCamera.parent.forward, directionToMonster);

        // Если игрок отвернулся (>160 градусов), монстр приближается
        if (angle > 160f)
        {
            float distance = Vector3.Distance(playerCamera.parent.position, monster.transform.position);
            if (distance > minDistance)
            {
                monster.transform.position = Vector3.MoveTowards(
                    monster.transform.position,
                    playerCamera.parent.position,
                    approachSpeed * Time.deltaTime
                );
            }
        }

        // Если игрок уходит слишком далеко, монстр исчезает
        if (Vector3.Distance(playerCamera.position, monster.transform.position) > disappearDistance)
        {
            DeactivateMonster();
        }
    }

    private void ActivateMonster()
    {
        if (!isMonsterActive)
        {
            Debug.Log("Монстр активирован!");
            monster.SetActive(true);
            audioSource.Play();
            isMonsterActive = true;
            monster.transform.position = playerCamera.parent.transform.position + playerCamera.parent.transform.forward * 5;
        }
    }

    private void DeactivateMonster()
    {
        if (isMonsterActive)
        {
            Debug.Log("Монстр исчезает!");
            monster.SetActive(false);
            isMonsterActive = false;
        }
    }
}
