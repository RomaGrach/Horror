using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterTrigger : MonoBehaviour
{
    public GameObject monster;       // Ссылка на объект монстра
    public Transform playerCamera;  // Ссылка на камеру игрока
    public float randomIntervalMin = 5f; // Минимальное время до следующего появления
    public float randomIntervalMax = 15f; // Максимальное время до следующего появления
    public float monsterDistance = 5f;   // Расстояние перед игроком

    private AudioSource monsterSound;  // Источник звука монстра

    private void Start()
    {
        // Убедимся, что камера указана
        if (playerCamera == null)
        {
            GameObject cameraObject = GameObject.Find("First Person Camera");
            if (cameraObject != null)
            {
                playerCamera = cameraObject.transform;
            }
            else
            {
                Debug.LogError("Камера с именем 'First Person Camera' не найдена!");
                return;
            }
        }

        // Проверяем наличие объекта монстра
        if (monster == null)
        {
            Debug.LogError("Монстр не привязан к скрипту!");
            return;
        }

        // Получаем AudioSource монстра
        monsterSound = monster.GetComponent<AudioSource>();
        if (monsterSound == null)
        {
            Debug.LogError("На объекте монстра отсутствует компонент AudioSource!");
            return;
        }

        // Запускаем цикл случайных появлений монстра
        StartCoroutine(TriggerMonsterAtRandom());
    }

    private IEnumerator TriggerMonsterAtRandom()
    {
        while (true)
        {
            // Ждём случайное время
            yield return new WaitForSeconds(Random.Range(randomIntervalMin, randomIntervalMax));

            // Показываем монстра
            ShowMonster();
        }
    }

    private void ShowMonster()
    {
        Debug.Log("Показываем монстра!");
        monster.transform.position = playerCamera.position + playerCamera.forward * 5f;
        monster.transform.rotation = Quaternion.LookRotation(playerCamera.forward);
        monster.SetActive(true);

        // Поворачиваем монстра лицом к игроку
        monster.transform.rotation = Quaternion.LookRotation(playerCamera.forward);

        // Активируем монстра
        monster.SetActive(true);

        // Проигрываем звук
        if (monsterSound != null)
        {
            monsterSound.Play();
        }

        // Скрываем монстра через короткое время
        StartCoroutine(HideMonster());
    }

    private IEnumerator HideMonster()
    {
        yield return new WaitForSeconds(0.5f); // Время, пока монстр остаётся видимым
        monster.SetActive(false);
    }
}
