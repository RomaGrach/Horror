using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public BackgroundMusic backgroundMusic;

    public void PlayGame()
    {
        // Остановить музыку
        if (backgroundMusic != null)
        {
            backgroundMusic.StopMusic();
        }

        // Переход на сцену TestMVP
        SceneManager.LoadScene("TestMVP");
    }
}
