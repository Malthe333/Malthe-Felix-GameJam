using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathScreen : MonoBehaviour
{
    public RawImage rawImage;

    public GameObject RetryButton;
    public float duration = 2f; // Time in seconds for full fade in

    private void Start()
    {
        // Start with fully transparent
        Color c = rawImage.color;
        c.a = 0f;
        rawImage.color = c;

    }

    public void StartEndScreen()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color c = rawImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            rawImage.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        {
            RetryButton.SetActive(true);
        }
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
