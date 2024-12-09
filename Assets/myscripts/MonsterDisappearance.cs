using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterDisappearance : MonoBehaviour
{
    private Renderer monsterRenderer;
    private Color originalColor;

    void Start()
    {
        monsterRenderer = GetComponent<Renderer>();
        originalColor = monsterRenderer.material.color;
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            Color color = originalColor;
            color.a = alpha;
            monsterRenderer.material.color = color;
            yield return null;
        }

        gameObject.SetActive(false); // Отключить монстра после исчезновения
    }
}
