using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class NoteManager : MonoBehaviour
{
    public List<GameObject> notes; // Список всех записок
    public GameObject pointer; // Картинка, которая будет указывать на ближайшую не открытую записку
    public List<GameObject> openedNotes = new List<GameObject>(); // Список открытых записок
    private int noteCounter = 0; // Счетчик открытых записок
    public TextMeshProUGUI signText;
    public int countNotes;
    public string targetObjectName = "Door04_pr"; // Имя целевого объекта
    public bool NOTopen = true;
    public GameObject targetObject;

    private void Start()
    {
        countNotes = notes.Count;
        SetSignText();
        targetObject = GameObject.Find(targetObjectName);
    }

    void Update()
    {
        RotatePointerToNearestNote();
        if(NOTopen && openedNotes.Count == notes.Count)
        {
            NOTopen = false;
            if (targetObject != null)
            {
                // Получить компонент RotateObject
                RotateObject rotateComponent = targetObject.GetComponent<RotateObject>();

                // Проверить, найден ли компонент RotateObject
                if (rotateComponent != null)
                {
                    // Вызвать функцию StartRotation
                    rotateComponent.StartRotation();
                    Debug.Log("StartRotation вызван у объекта: " + targetObjectName);
                }
                else
                {
                    Debug.LogError("Компонент RotateObject не найден у объекта: " + targetObjectName);
                }
            }
            else
            {
                Debug.LogError("Объект с именем " + targetObjectName + " не найден.");
            }
        }

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
        else
        {
            pointer.transform.rotation = Quaternion.Slerp(pointer.transform.rotation, Quaternion.LookRotation(transform.up), Time.deltaTime * 5f);
        }
    }

    void SetSignText()
    {
        signText.text = "Собери " + countNotes.ToString() + " вентилей\nСобрано: " + openedNotes.Count.ToString();
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
            SetSignText();
        }
    }

    public void OpenNoteByScript(Note noteScript)
    {
        if (!openedNotes.Contains(noteScript.gameObject))
        {
            openedNotes.Add(noteScript.gameObject);
            noteCounter++;
            Debug.Log("Note opened! Total opened notes: " + noteCounter);
            SetSignText();
        }
    }
}


