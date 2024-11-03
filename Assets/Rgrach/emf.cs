using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class emf : MonoBehaviour
{
    public GameObject[] levels;
    public playerCloc playerCloc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        activaitLEVEL(playerCloc.moshrum);
    }

    private void OnTriggerEnter(Collider other)
    {

        moshrums mushroomsScript = other.GetComponent<moshrums>();
        if (mushroomsScript != null)
        {
            activaitLEVEL(mushroomsScript.touchingObjectsCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < 5; i++)
        {
            levels[i].SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        moshrums mushroomsScript = other.GetComponent<moshrums>();
        if (mushroomsScript != null)
        {
            activaitLEVEL(mushroomsScript.touchingObjectsCount);
        }
    }

    public void activaitLEVEL(int a)
    {
        for (int i = 0; i < 5; i++)
        {
            levels[i].SetActive(false);
        }
        for (int i = 0; i < a && i < 5; i++)
        {
            levels[i].SetActive(true);
        }
    }
}
