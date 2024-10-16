using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatorController : MonoBehaviour
{
    public List<moshrums> cells; // Список всех клеток на сцене
    public int totalMines = 10; // Общее количество мин
    public int fieldWidth = 10; // Ширина поля
    public int fieldHeight = 10; // Высота поля
    public float mineDensity = 0.1f; // Процентное соотношение мин

    void Awake()
    {
        totalMines = Mathf.RoundToInt(cells.Count * mineDensity);
        GenerateMines();
    }

    void GenerateMines()
    {
        // Проверка на корректность параметров
        if (totalMines > cells.Count)
        {
            Debug.LogError("Количество мин превышает количество клеток!");
            return;
        }

        // Сброс всех мин
        foreach (var cell in cells)
        {
            cell.dangare = false;
        }

        // Случайное распределение мин
        List<int> mineIndices = new List<int>();
        while (mineIndices.Count < totalMines)
        {
            int randomIndex = Random.Range(0, cells.Count);
            if (!mineIndices.Contains(randomIndex))
            {
                mineIndices.Add(randomIndex);
                cells[randomIndex].dangare = true;
                cells[randomIndex].tag = "ГрипЯд"; // Устанавливаем тег для опасных клеток
            }
        }

        Debug.Log("Мины успешно распределены!");
    }
}
