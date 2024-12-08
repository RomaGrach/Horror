using UnityEngine;

public class Note : MonoBehaviour
{
    private NoteManager noteManager;

    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            noteManager.OpenNoteByScript(this);
            // Дополнительная логика для открытия записки (например, отображение текста)
            Debug.Log("Note triggered by player!");
            gameObject.SetActive(false);
        }
    }
}