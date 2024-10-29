using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;     // ������ �����
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);    // ��������� ������ ��� ������
    }

    void Update()
    {
        // ��������� ������� ������� ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // ���� ����� �������, ���������� ����
            }
            else
            {
                PauseGame(); // ���� ����� ���, ������������ �
            }
        }
    }

    // ������� ��� ������ ������ �����
    public void PauseGame()
    {
        pausePanel.SetActive(true);   // �������� ������ �����
        Time.timeScale = 0f;          // ���������� ����� � ����
        isPaused = true;
    }

    // ������� ��� ����������� ����
    public void ResumeGame()
    {
        pausePanel.SetActive(false);   // ������ ������ �����
        Time.timeScale = 1f;           // ����������� �����
        isPaused = false;
    }

    // ������� � ������� ����
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;            // ����������� ����� ����� ��������� ����
        SceneManager.LoadScene("menu"); // �������� "MainMenu" �� �������� ����� �����
    }

}
