using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moshrums : MonoBehaviour
{
    public int touchingObjectsCount = -1;

    public bool dangare = false;

    public List<Material> materials;

    private Renderer objectRenderer;



    // Радиус сферы для проверки соприкосновения
    public Vector3 checkBoxSize = new Vector3(1.0f, 1.0f, 1.0f);

    // Метод, вызываемый при старте

    private void Awake()
    {
        if (dangare)
        {
            tag = "ГрипЯд";
        }
    }

    void Start()
    {
        
        objectRenderer = GetComponent<Renderer>();
        // Инициализация переменной
        touchingObjectsCount = 0;

        // Получаем все коллайдеры, которые соприкасаются с текущим объектом
        Collider[] hitColliders = Physics.OverlapBox(transform.position, checkBoxSize / 2);

        // Проходим по всем найденным коллайдерам
        foreach (Collider collider in hitColliders)
        {
            // Проверяем, что найденный коллайдер не является коллайдером самого объекта
            if (collider.gameObject != gameObject && collider.tag == "ГрипЯд")
            {
                touchingObjectsCount++;
            }
        }
    }

    // Метод для визуализации сферы в редакторе
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, checkBoxSize);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (dangare)
            {
                objectRenderer.material = materials[1];
            }
            else
            {
                objectRenderer.material = materials[0];
            }
        }
        
    }
}
