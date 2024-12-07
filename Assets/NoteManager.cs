using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public List<GameObject> notes; // Список всех записок
    public GameObject pointer; // Картинка, которая будет указывать на ближайшую не открытую записку
    private List<GameObject> openedNotes = new List<GameObject>(); // Список открытых записок
    private int noteCounter = 0; // Счетчик открытых записок

    void Update()
    {
        RotatePointerToNearestNote();
    }

    void RotatePointerToNearestNote()
    {
        GameObject nearestNote = GetNearestNote();
        if (nearestNote != null)
        {
            Vector3 direction = nearestNote.transform.position - pointer.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            pointer.transform.rotation = Quaternion.Slerp(pointer.transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    GameObject GetNearestNote()
    {
        GameObject nearestNote = null;
        float minDistance = float.MaxValue;

        foreach (GameObject note in notes)
        {
            if (!openedNotes.Contains(note))
            {
                float distance = Vector3.Distance(pointer.transform.position, note.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestNote = note;
                }
            }
        }

        return nearestNote;
    }

    public void OpenNoteGameObject(GameObject note)
    {
        if (!openedNotes.Contains(note))
        {
            openedNotes.Add(note);
            noteCounter++;
            Debug.Log("Note opened! Total opened notes: " + noteCounter);
        }
    }

    public void OpenNoteByScript(Note noteScript)
    {
        if (!openedNotes.Contains(noteScript.gameObject))
        {
            openedNotes.Add(noteScript.gameObject);
            noteCounter++;
            Debug.Log("Note opened! Total opened notes: " + noteCounter);
        }
    }
}