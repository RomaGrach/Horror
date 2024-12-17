using UnityEngine;

public class ActivateObjectOnStart : MonoBehaviour
{
    public GameObject objectToActivate; // Объект, который нужно активировать

    public GameObject player;
    void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);

        }
        else
        {
            Debug.LogError("Object to activate is not assigned.");
        }
        if (player != null)
        {
            player.GetComponent<death>().deathS();

        }
        else
        {
            Debug.LogError("player");
        }
    }
}
