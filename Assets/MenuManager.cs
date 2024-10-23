using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public BackgroundMusic backgroundMusic;

    public void PlayGame()
    {
        // ���������� ������
        if (backgroundMusic != null)
        {
            backgroundMusic.StopMusic();
        }

        // ������� �� ����� TestMVP
        SceneManager.LoadScene("TestMVP");
    }
}
