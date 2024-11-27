using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monsterFish : MonoBehaviour
{
    public float detectionRadius = 10f; // Радиус обнаружения игрока
    private NavMeshAgent navMeshAgent;
    private Transform player;
    public float hightDef = -7.89f;
    public float hightAtack = -5.74f;
    public float hightSpeed = -0.1f;
    public GameObject body;
    public Animator animator; // Ссылка на аниматор
    public Transform objektToLook;
    public float rotationSpeed = 2.0f; // Скорость поворота
    private bool isPlayerDestroyed = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                navMeshAgent.SetDestination(player.position);

                if (body.transform.position.y < hightAtack)
                {
                    body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y + hightSpeed * Time.deltaTime, body.transform.position.z);
                }
            }
            else
            {
                navMeshAgent.ResetPath(); // Останавливаем движение, если игрок вне радиуса обнаружения

                if (body.transform.position.y > hightDef)
                {
                    body.transform.position = new Vector3(body.transform.position.x, body.transform.position.y - hightSpeed * Time.deltaTime, body.transform.position.z);
                }
            }
        }

        if (isPlayerDestroyed)
        {
            //SmoothRotatePlayer();
            SmoothRotateCamera();
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (animator != null)
            {
                DestroyPlayer();
                animator.SetTrigger("byte");
                isPlayerDestroyed = true;
            }
        }
    }

    public void DestroyPlayer()
    {
        navMeshAgent.speed = 2;
        player.gameObject.GetComponent<FirstPersonMovement>().enabled = false;
        player.gameObject.GetComponent<Crouch>().enabled = false;
        player.gameObject.GetComponent<Jump>().enabled = false;
        //player.gameObject.GetComponent<FirstPersonLook>().enabled = false; // Отключаем управление игроком
        //GameObject camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        // Поворот игрока по оси Y в сторону objektToLook

        /*
        Vector3 directionToLook = objektToLook.position - player.position;
        directionToLook.y = 0; // Игнорируем ось Y для поворота по горизонтали
        player.rotation = Quaternion.LookRotation(directionToLook);

        // Поворот камеры по оси X в сторону objektToLook
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera != null)
        {
            camera.GetComponent<FirstPersonLook>().enabled = false; // Отключаем управление игроком
            Vector3 cameraDirectionToLook = objektToLook.position - camera.transform.position;
            cameraDirectionToLook.x = 0; // Игнорируем ось X для поворота по вертикали
            camera.transform.rotation = Quaternion.LookRotation(cameraDirectionToLook);
        }
        */

    }

    private void SmoothRotatePlayer()
    {
        Vector3 directionToLook = objektToLook.position - player.position;
        directionToLook.x = 0; // Игнорируем ось Y для поворота по горизонтали
        directionToLook.z = 0; // Игнорируем ось Y для поворота по горизонтали
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void SmoothRotateCamera()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.parent = null;
        if (camera != null)
        {
            camera.GetComponent<FirstPersonLook>().enabled = false; // Отключаем управление игроком
            Vector3 cameraDirectionToLook = objektToLook.position - camera.transform.position;
            //cameraDirectionToLook.y = 0; // Игнорируем ось X для поворота по вертикали
            Quaternion targetCameraRotation = Quaternion.LookRotation(cameraDirectionToLook);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, targetCameraRotation, rotationSpeed * Time.deltaTime);
        }
    }
}