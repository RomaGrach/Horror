using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class leninActivator : MonoBehaviour
{
    public GameObject[] objToActivate;
    public GameObject[] objToDeactivate;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // Проверка на тег "Player"
        if (other.CompareTag("Player"))
        {
            foreach (GameObject ob in objToActivate)
            {
                ob.SetActive(true);
            }
            foreach (GameObject ob in objToDeactivate)
            {
                ob.SetActive(false);
            }

            GetLenin GL = other.GetComponent<GetLenin>();
            GL.GetLeninFunc();

        }
    }
}
