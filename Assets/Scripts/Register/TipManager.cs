using UnityEngine;
using TMPro;
using System.Collections;

public class TipManager : MonoBehaviour
{
    public int totalTips = 0;
    public TextMeshProUGUI tipText; // Assign in Inspector

    public float displayDuration = 1.5f;
    public float fadeDuration = 0.5f;

    private Coroutine currentFade;

    public void AddTip(int amount)
    {
        totalTips += amount;
        Debug.Log($"💰 Received tip: {amount} | Total: {totalTips}");

        if (tipText != null)
        {
            tipText.text = $"+ {amount}";
            tipText.alpha = 1f;

            if (currentFade != null)
                StopCoroutine(currentFade);

            currentFade = StartCoroutine(FadeOutText());
        }
    }

    IEnumerator FadeOutText()
    {
        yield return new WaitForSeconds(displayDuration);

        float elapsed = 0f;
        Color originalColor = tipText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            tipText.alpha = alpha;
            yield return null;
        }

        tipText.text = "";
        tipText.alpha = 1f;
    }

    public void ClearTipDisplay()
    {
        if (tipText != null)
        {
            tipText.text = "";
            tipText.alpha = 1f;
        }
    }

    void Start()
    {
        ClearTipDisplay();
    }
}
