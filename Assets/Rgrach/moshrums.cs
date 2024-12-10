using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moshrums : MonoBehaviour
{
    public int touchingObjectsCount = -1;

    public bool dangare = false;

    public List<Material> materials;

    private Renderer objectRenderer;

    public GameObject discoveryParticlesPrefab; // Префаб эффекта частиц

    public GroundCheck groundCheck;
    public FirstPersonMovement s_g;

    public event System.Action Jumped;

    public float timeAtakNow = 0f;
    public float timeAtak = 1f;

    public float knockbackForce = 10f; // Сила отбрасывания
    public float knockbackUpwardForce = 2f; // Сила отбрасывания вверх

    // Радиус сферы для проверки соприкосновения
    public Vector3 checkBoxSize = new Vector3(1.0f, 1.0f, 1.0f);

    public bool waitGround = false;

    // Метод, вызываемый при старте

    private void Update()
    {
        if (timeAtak > timeAtakNow)
        {
            timeAtakNow += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (waitGround && !(timeAtak > timeAtakNow))
        {
            if (groundCheck.isGrounded)
            {
                //Debug.Log(groundCheck.isGrounded);
                s_g.enabled = true;
            }
        }
    }


    private void Awake()
    {
        if (dangare)
        {
            tag = "ГрипЯд";
        }
    }

    void Start()
    {
        
        objectRenderer = GetComponent<Renderer>();
        // Инициализация переменной
        touchingObjectsCount = 0;

        // Получаем все коллайдеры, которые соприкасаются с текущим объектом
        Collider[] hitColliders = Physics.OverlapBox(transform.position, checkBoxSize / 2);

        // Проходим по всем найденным коллайдерам
        foreach (Collider collider in hitColliders)
        {
            // Проверяем, что найденный коллайдер не является коллайдером самого объекта
            if (collider.gameObject != gameObject && collider.tag == "ГрипЯд")
            {
                touchingObjectsCount++;
            }
        }
    }

    // Метод для визуализации сферы в редакторе
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, checkBoxSize);
        if (dangare)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            if (dangare)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                // Если компонент PlayerHealth найден, вызываем метод TakeDamage
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage();
                }
                objectRenderer.material = materials[1];
                ApplyKnockback(other);
                if (discoveryParticlesPrefab != null)
                {
                    Instantiate(discoveryParticlesPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                objectRenderer.material = materials[0];
            }
        }
        
    }

    private void ApplyKnockback(Collider player)
    {
        // Получаем компонент Rigidbody у игрока
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        s_g = player.GetComponent<FirstPersonMovement>();
        groundCheck = player.GetComponentInChildren<GroundCheck>();

        // Если компонент Rigidbody найден, применяем силу отбрасывания
        if (playerRigidbody != null)
        {
            Vector3 knockbackDirection = (player.transform.position - transform.position).normalized;
            knockbackDirection.y = 0; // Убираем вертикальную составляющую
            Vector3 knockback = knockbackDirection * knockbackForce + Vector3.up * knockbackUpwardForce;
            Jumped?.Invoke();
            s_g.enabled = false;
            //playerRigidbody.AddForce(transform.forward * 100 * knockbackForce, ForceMode.Impulse);
            playerRigidbody.AddForce(transform.up * 100 * knockbackUpwardForce + -1 * player.transform.forward * 100 * knockbackForce);
            //playerRigidbody.AddForce(Vector3.up * 100 * 5);
            //playerRigidbody.AddForce(transform.up * 100 * 5 + transform.forward * 100 * 5);
            //s_g.enabled = true;
            waitGround = true;
            Jumped?.Invoke();

            timeAtakNow = 0f;
        }
    }
}
