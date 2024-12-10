using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extractChildran : MonoBehaviour
{
    void Start()
    {
        UnparentAllChildren();
    }

    void UnparentAllChildren()
    {
        // Создаем список для хранения всех дочерних объектов
        List<Transform> children = new List<Transform>();

        // Заполняем список всеми дочерними объектами
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        // Перебираем все объекты в списке и устанавливаем их родителя на null
        foreach (Transform child in children)
        {
            child.SetParent(null);
            Debug.Log("Unparented: " + child.name);
        }
    }
}



