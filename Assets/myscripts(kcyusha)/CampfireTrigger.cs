using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampfireTrigger : MonoBehaviour
{
    private Text messageText;

    private void Start()
    {
        // Находим объект текста по имени
        messageText = GameObject.Find("CampfireText").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "Ох...что-то я устал, мне стоит согреться и отдохнуть";
            messageText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.enabled = false;
        }
    }
}
