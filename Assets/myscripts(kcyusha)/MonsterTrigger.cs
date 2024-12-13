using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    public GameObject monster;       // Ссылка на объект монстра
    public Transform playerCamera;   // Ссылка на камеру игрока
    public float randomIntervalMin = 5f; // Минимальный интервал до появления монстра
    public float randomIntervalMax = 15f; // Максимальный интервал до появления монстра
    public float monsterDistance = 5f;   // Расстояние появления монстра
    public bool monsterAvailable = false; // Доступность монстра
    private Quaternion playerStartAngle; // Начальный угол игрока

    private AudioSource monsterSound;  // Звуковой эффект монстра

    private void Start()
    {
        monsterAvailable = false;

        // Проверка, что камера игрока указана
        if (playerCamera == null)
        {
            GameObject cameraObject = GameObject.Find("First Person Camera");
            if (cameraObject != null)
            {
                playerCamera = cameraObject.transform;
            }
            else
            {
                Debug.LogError("Ошибка: Камера 'First Person Camera' не найдена!");
                return;
            }
        }

        // Проверка, что объект монстра указан
        if (monster == null)
        {
            Debug.LogError("Ошибка: Монстр не назначен!");
            return;
        }

        // Получение компонента AudioSource монстра
        monsterSound = monster.GetComponent<AudioSource>();
        if (monsterSound == null)
        {
            Debug.LogError("Ошибка: На монстре отсутствует компонент AudioSource!");
            return;
        }

        // Запуск корутины для появления монстра
        StartCoroutine(TriggerMonsterAtRandom());
    }

    private void Update()
    {
        if (monsterAvailable)
        {
            float angleDifference = Quaternion.Angle(playerStartAngle, playerCamera.parent.rotation);
            if (angleDifference > 160f) // Выполнить, когда игрок развернется на 180 градусов
            {
                ShowMonster();
                monsterAvailable = false; // Сброс состояния доступности монстра
            }
        }
    }

    private IEnumerator TriggerMonsterAtRandom()
    {
        while (true)
        {
            // Ждать случайное время
            yield return new WaitForSeconds(Random.Range(randomIntervalMin, randomIntervalMax));

            // Подготовка к появлению монстра
            monsterAvailable = true;
            playerStartAngle = playerCamera.parent.rotation;
        }
    }

    private void ShowMonster()
    {
        Debug.Log("Появление монстра!");
        monster.transform.position = playerCamera.parent.position + playerCamera.parent.forward * monsterDistance;
        monster.transform.rotation = Quaternion.LookRotation(playerCamera.parent.forward);
        monster.SetActive(true);

        // Проигрывание звука монстра
        if (monsterSound != null)
        {
            monsterSound.Play();
        }

        // Скрытие монстра через 0.5 секунд
        StartCoroutine(HideMonster());
    }

    private IEnumerator HideMonster()
    {
        yield return new WaitForSeconds(0.5f); // Время, через которое монстр исчезнет
        monster.SetActive(false);
    }
}
