using System.Collections;
using UnityEngine;

public class FadeAnim : MonoBehaviour
{
    public float fadeInTime = 3f;
    public float visibleTime = 2f;
    public float fadeOutTime = 3f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInOutRoutine());
    }

    IEnumerator FadeInOutRoutine()
    {
        while (true)
        {
            // Fade in
            yield return StartCoroutine(FadeIn());
            // Stay visible for a while
            yield return new WaitForSeconds(visibleTime);
            // Fade out
            yield return StartCoroutine(FadeOut());
            // Stay invisible for a while
            yield return new WaitForSeconds(fadeOutTime);
        }
    }

    IEnumerator FadeIn()
    {
        float time = 0;
        while (time < fadeInTime)
        {
            float alpha = time / fadeInTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator FadeOut()
    {
        float time = 0;
        while (time < fadeOutTime)
        {
            float alpha = 1f - (time / fadeOutTime);
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = new Color(1, 1, 1, 0);
    }

    public void ResetAnim()
    {
        StopAllCoroutines();
        spriteRenderer.color = new Color(1, 1, 1, 0);
        gameObject.SetActive(true);
        StartCoroutine(FadeInOutRoutine());
    }
}
