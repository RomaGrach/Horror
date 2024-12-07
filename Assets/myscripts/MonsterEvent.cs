using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterEvent : MonoBehaviour
{
    public GameObject monster; // Ссылка на объект монстра
    public Transform monsterPosition; // Позиция, где появляется монстр
    public float lookSpeed = 2f; // Скорость поворота камеры
    public float monsterDisappearDelay = 3f; // Время до исчезновения монстра
    public AudioClip tensionSound; // Напряжённый звук

    private bool eventTriggered = false; // Чтобы событие происходило только один раз
    private AudioSource audioSource; // Источник звука
    private Transform playerCamera; // Камера игрока

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Ищем камеру по имени
        GameObject cameraObject = GameObject.Find("First Person Camera");
        if (cameraObject != null)
        {
            playerCamera = cameraObject.transform;
        }
        else
        {
            Debug.LogError("Камера 'First Person Camera' не найдена!");
        }

        if (monster != null)
        {
            monster.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (eventTriggered) return;

        // Если игрок входит в триггер
        if (other.CompareTag("Player"))
        {
            eventTriggered = true;
            StartCoroutine(MonsterSequence());
        }
    }

    private IEnumerator MonsterSequence()
    {
        // Включаем монстра
        if (monster != null)
        {
            monster.SetActive(true);
            monster.transform.position = monsterPosition.position;
        }

        // Воспроизводим напряжённый звук
        if (audioSource != null && tensionSound != null)
        {
            audioSource.clip = tensionSound;
            audioSource.Play();
        }

        // Поворачиваем камеру на монстра
        Vector3 directionToMonster = (monsterPosition.position - playerCamera.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(directionToMonster);

        float time = 0;
        while (time < 1f)
        {
            time += Time.deltaTime * lookSpeed;
            playerCamera.rotation = Quaternion.Slerp(playerCamera.rotation, targetRotation, time);
            yield return null;
        }

        // Задержка перед исчезновением монстра
        yield return new WaitForSeconds(monsterDisappearDelay);

        // Отключаем монстра
        if (monster != null)
        {
            monster.SetActive(false);
        }
    }
}

