using UnityEngine;

public class Note : MonoBehaviour
{
    private NoteManager noteManager;
    public GameObject discoveryParticlesPrefab; // Префаб эффекта частиц

    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            noteManager.OpenNoteByScript(this);
            if (discoveryParticlesPrefab != null) {
                Instantiate(discoveryParticlesPrefab, transform.position, Quaternion.identity);
            }            // Дополнительная логика для открытия записки (например, отображение текста)
            Debug.Log("Note triggered by player!");
            gameObject.SetActive(false);
        }
    }
}