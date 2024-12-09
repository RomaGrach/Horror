using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public Transform target; // Цель, за которой следует монстр (например, игрок)
    private NavMeshAgent agent; // Компонент NavMeshAgent

    void Start()
    {
        // Получаем компонент NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Проверяем, установлен ли NavMeshAgent
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent не найден на объекте " + gameObject.name);
        }
    }

    void Update()
    {
        // Если цель (игрок) установлена, задаем позицию для NavMeshAgent
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
