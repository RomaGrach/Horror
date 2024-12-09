using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerActivateMonster : MonoBehaviour
{
    public GameObject monster; // Объект монстра
    public AudioSource scareSound; // Аудиоисточник со страшным звуком
    public Transform player; // Ссылка на объект игрока

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, входит ли игрок в область триггера
        if (other.CompareTag("Player"))
        {
            // Активируем объект монстра
            if (monster != null)
            {
                monster.SetActive(true);

                // Передаем игрока в скрипт движения монстра
                MonsterMovement movementScript = monster.GetComponent<MonsterMovement>();
                if (movementScript != null)
                {
                    movementScript.target = player; // Устанавливаем игрока как цель
                }
            }

            // Проигрываем страшный звук
            if (scareSound != null)
            {
                scareSound.Play();
            }
        }
    }
}
