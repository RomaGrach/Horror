using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemInteraction : MonoBehaviour
{
    public GameObject choicePanel; // Ссылка на панель с выбором
    private bool isPlayerNearby = false; // Проверка на близость игрока

    void Update()
    {
        // Если игрок рядом и нажал кнопку взаимодействия (например, "E")
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowChoicePanel();
        }
    }

    private void ShowChoicePanel()
    {
        // Показываем панель выбора
        choicePanel.SetActive(true);
        Time.timeScale = 0f; // Ставим игру на паузу
    }

    public void EatItem()
    {
        // Действие "Съесть"
        Debug.Log("Вы съели предмет!");
        CloseChoicePanel();
    }

    public void LeaveItem()
    {
        // Действие "Оставить"
        Debug.Log("Вы оставили предмет!");
        CloseChoicePanel();
    }

    private void CloseChoicePanel()
    {
        // Закрываем панель выбора
        choicePanel.SetActive(false);
        Time.timeScale = 1f; // Возобновляем игру
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, если игрок рядом
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Игрок ушел
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}

