using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public Image damageImage;
    public Image damageImage1;
    public Image deathImage;
    public TextMeshProUGUI deathText;
    public float flashSpeed = 0.5f; // Полсекунды
    public float flashSpeed1 = 0.2f; // Полсекунды
    public float damageImage1Trans = 0.3f;
    public int damag = 0;
    public float timer = 0f;
    public float timerWAIT = 10f;
    private bool isDead;
    private bool damaged;
    public GameObject canvasDEAD;
    public death death;

    void Awake()
    {
        damag = 0;
        SetImageAlpha(damageImage, 0f);
        SetImageAlpha(damageImage1, 0f);
        SetImageAlpha(deathImage, 0f);
        SetTextAlpha(deathText, 0f);
        canvasDEAD.SetActive(false);
    }

    void Update()
    {
        if (damag == 1)
        {
            SetImageAlpha(damageImage, Mathf.Lerp(damageImage.color.a, 1f, flashSpeed1 * Time.deltaTime));
            SetImageAlpha(damageImage1, Mathf.Lerp(damageImage1.color.a, damageImage1Trans, flashSpeed1 * Time.deltaTime));
            timer += Time.deltaTime;
        }
        else
        {
            SetImageAlpha(damageImage, Mathf.Lerp(damageImage.color.a, 0f, flashSpeed * Time.deltaTime));
            SetImageAlpha(damageImage1, Mathf.Lerp(damageImage1.color.a, 0f, flashSpeed1 * Time.deltaTime));
            //SetTextAlpha(damageText, Mathf.Lerp(damageText.color.a, 0f, flashSpeed * Time.deltaTime));
        }
        if (timer >= timerWAIT)
        {
            timer = 0;
            damag = 0;
        }
        if (damag == 2 && !isDead)
        {
            Death();
        }



    }

    public void TakeDamage()
    {
        damag += 1;
        //damaged = true;
        //currentHealth -= amount;

        if (damag == 2 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        canvasDEAD.SetActive(true);
        isDead = true;
        SetImageAlpha(deathImage, 1f);
        SetTextAlpha(deathText, 1f);
        death.deathS();
        // Здесь можно добавить дополнительные действия при смерти игрока
    }

    void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}