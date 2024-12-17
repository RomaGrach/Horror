using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekPointObj : MonoBehaviour
{
    public int CheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // ���� ������, �������� � �������, ����� ��� "Player"
        if (other.CompareTag("Player"))
        {
            if(CheckPoint > Progress.Instance.PlayerInfo.LevelProgress)
            {
                Progress.Instance.PlayerInfo.LevelProgress = CheckPoint;
                Progress.Instance.SaveProgres();
                Debug.Log("LevelProgress " + Progress.Instance.PlayerInfo.LevelProgress);
            }
            
        }
    }
}
