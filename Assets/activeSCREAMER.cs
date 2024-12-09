using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeSCREAMER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerSCREAMERS screamer = other.GetComponent<playerSCREAMERS>();
            screamer.ActivScreamer();
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
