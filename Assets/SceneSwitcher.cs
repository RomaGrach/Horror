using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Функция для переключения на заданную сцену



    public void SwitchToScene(int sceneName)
    {
        
            SceneManager.LoadScene(sceneName);
        
        
    }
}
