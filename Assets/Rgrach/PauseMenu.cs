using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;     // Панель паузы
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);    // Отключаем панели при старте
    }

    void Update()
    {
        // Проверяем нажатие клавиши ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Если пауза активна, продолжить игру
            }
            else
            {
                PauseGame(); // Если паузы нет, активировать её
            }
        }
    }

    // Функция для показа панели паузы
    public void PauseGame()
    {
        pausePanel.SetActive(true);   // Показать панель паузы
        Time.timeScale = 0f;          // Остановить время в игре
        isPaused = true;
    }

    // Функция для продолжения игры
    public void ResumeGame()
    {
        pausePanel.SetActive(false);   // Скрыть панель паузы
        Time.timeScale = 1f;           // Возобновить время
        isPaused = false;
    }

    // Переход в главное меню
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;            // Возобновить время перед загрузкой меню
        SceneManager.LoadScene("menu"); // Замените "MainMenu" на название вашей сцены
    }

}
