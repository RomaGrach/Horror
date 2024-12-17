using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class altar : MonoBehaviour
{

    public Animator animator; // Ссылка на компонент анимации

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
        // Проверка на тег "Player"
        if (other.CompareTag("Player"))
        {

            GetLenin GL = other.GetComponent<GetLenin>();
            if (GL.lenin)
            {
                GL.DropLeninFunc();
                startAction(); // Запуск анимации
            }
            

        }
    }

    public void startAction()
    {
        animator.SetTrigger("act"); // Установка булевого параметра 'act' в true для запуска анимаци
    }

}
