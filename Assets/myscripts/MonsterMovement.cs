using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public Transform target; // ����, �� ������� ������� ������ (��������, �����)
    private NavMeshAgent agent; // ��������� NavMeshAgent

    void Start()
    {
        // �������� ��������� NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // ���������, ���������� �� NavMeshAgent
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent �� ������ �� ������� " + gameObject.name);
        }
    }

    void Update()
    {
        // ���� ���� (�����) �����������, ������ ������� ��� NavMeshAgent
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
