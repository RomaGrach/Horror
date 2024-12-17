using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> prefabsToSpawn; // Список префабов для спавна
    public Vector3 spawnAreaMin; // Минимальная граница области спавна
    public Vector3 spawnAreaMax; // Максимальная граница области спавна
    public float initialSpawnRate = 1f; // Начальная скорость спавна объектов (количество объектов в секунду)
    public float spawnRateIncrease = 0.1f; // Увеличение скорости спавна объектов
    private float nextSpawnTime;
    public float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + currentSpawnRate;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            //currentSpawnRate += spawnRateIncrease;
            nextSpawnTime = Time.time + currentSpawnRate;
        }
    }

    void SpawnObject()
    {
        // Генерация случайной позиции в заданной области спавна
        float x = Random.Range(transform.position.x - spawnAreaMax.x, transform.position.x + spawnAreaMax.x);
        float y = Random.Range(transform.position.y  , transform.position.y +spawnAreaMax.y);
        float z = Random.Range(transform.position.z - spawnAreaMax.z, transform.position.z + spawnAreaMax.z);
        Vector3 spawnPosition = new Vector3(x, y, z);

        // Выбор случайного префаба из списка
        int randomIndex = Random.Range(0, prefabsToSpawn.Count);
        GameObject prefabToSpawn = prefabsToSpawn[randomIndex];

        // Спавн выбранного префаба в случайной позиции
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        // Рисуем область спавна
        Gizmos.color = Color.red;
        Vector3 center = transform.position;
        Vector3 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawWireCube(center, size);
    }

}
