using UnityEngine;
using System.Collections;

public class Screamer : MonoBehaviour
{
    public GameObject monster; // Ссылка на объект монстра
    public Transform playerCamera; // Ссылка на камеру игрока
    public float distanceFromCamera = 2.0f; // Расстояние перед камерой, где появится монстр
    public string scareAnimationName = "Scare"; // Название анимации налёта

    private Animator monsterAnimator;

    void Start()
    {
        if (monster != null)
        {
            monsterAnimator = monster.GetComponent<Animator>();
            if (monsterAnimator == null)
            {
                Debug.LogError("Animator component not found on the monster object.");
            }
        }
        else
        {
            Debug.LogError("Monster object is not assigned.");
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned.");
        }

        if (monster != null && playerCamera != null && monsterAnimator != null)
        {/*
            // Позиционируем монстра перед камерой игрока
            Vector3 spawnPosition = playerCamera.position + playerCamera.forward * distanceFromCamera;
            monster.transform.position = spawnPosition;

            // Поворачиваем монстра, чтобы он смотрел на игрока
            monster.transform.LookAt(playerCamera);

            // Активируем монстра и запускаем анимацию
            monster.SetActive(true);
            */
            transform.position = playerCamera.parent.transform.position + playerCamera.parent.transform.forward * distanceFromCamera;
            transform.rotation = Quaternion.LookRotation(playerCamera.parent.transform.forward);
            
            if (monsterAnimator.HasState(0, Animator.StringToHash(scareAnimationName)))
            {
                monsterAnimator.Play(scareAnimationName);
            }
            else
            {
                Debug.LogError("Scare animation not found in the Animator.");
            }

            // Запускаем корутину для деактивации монстра после завершения анимации
            StartCoroutine(DeactivateMonsterAfterAnimation());
        }
    }

    

    private IEnumerator DeactivateMonsterAfterAnimation()
    {
        if (monsterAnimator != null)
        {
            // Ждём завершения анимации
            yield return new WaitForSeconds(monsterAnimator.GetCurrentAnimatorStateInfo(0).length);

            // Деактивируем монстра
            monster.SetActive(false);

            // Деактивируем триггер, чтобы скример не срабатывал повторно
            gameObject.SetActive(false);
        }
    }
}