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
    //"������ ����"
    public void StartGame()
    {
        // �������� "GameScene" �� �������� ����� ����� � �����
        SceneManager.LoadScene("TestMVP");
    }

    //"���������"
    public void OpenSettings()
    {
        // ����� ����� ������� ��������� ���� �������� ��� �������� UI ��������
        Debug.Log("������� ���� ��������");
    }

    // "��������� ����������"
    public void LoadGame()
    {
        // ����� ������ ���� ������ �������� ����������� ����
        Debug.Log("��������� ����������");
        // ��������, ����� ������� ����� �������� ������������ ���������
        // SaveSystem.LoadGame();
    }

    // ����� ��� ������ "�����" (���� �����)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("���� ���������");
    }
}
