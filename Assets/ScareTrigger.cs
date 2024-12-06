using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScareTrigger2 : MonoBehaviour
{
    public GameObject monster;         // Ссылка на модель монстра
    public Transform startPosition;    // Точка, откуда монстр начнёт движение
    public Transform endPosition;      // Точка, где монстр завершит движение
    public float runSpeed = 5f;        // Скорость монстра
    public AudioSource scareSound;     // Звук, который проигрывается при срабатывании триггера
    private bool hasTriggered = false; // Флаг, чтобы триггер срабатывал один раз

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) // Проверяем, столкнулся ли игрок
        {
            hasTriggered = true;

            // Включить звук
            if (scareSound != null)
            {
                scareSound.Play();
            }

            // Запустить монстра
            StartCoroutine(MoveMonster());
        }
    }

    private System.Collections.IEnumerator MoveMonster()
    {
        // Активировать монстра и установить начальную позицию
        monster.SetActive(true);
        monster.transform.position = startPosition.position;

        // Двигать монстра к конечной точке
        while (Vector3.Distance(monster.transform.position, endPosition.position) > 0.1f)
        {
            monster.transform.position = Vector3.MoveTowards(
                monster.transform.position,
                endPosition.position,
                runSpeed * Time.deltaTime
            );
            yield return null; // Ждём один кадр
        }

        // Скрыть монстра после завершения движения
        monster.SetActive(false);
    }
}

