/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //"Начать игру"
    public void StartGame()
    {
        // Замените "GameScene" на название вашей сцены с игрой
        SceneManager.LoadScene("TestMVP");
    }

    //"Настройки"
    public void OpenSettings()
    {
        // Здесь можно открыть отдельное меню настроек или показать UI элементы
        Debug.Log("Открыть меню настроек");
    }

    // "Загрузить сохранение"
    public void LoadGame()
    {
        // Здесь должна быть логика загрузки сохраненной игры
        Debug.Log("Загрузить сохранение");
        // Например, можно вызвать метод загрузки сохраненного состояния
        // SaveSystem.LoadGame();
    }

    // Метод для кнопки "Выход" (если будет)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Игра завершена");
    }
}
