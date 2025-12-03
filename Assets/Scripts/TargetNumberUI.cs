using UnityEngine;
using TMPro;
using System.Collections;

public class TargetNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetNumberTxt;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private float scaleUpFactor = 1.2f;
    [SerializeField] private float animationDuration = 0.3f;

    private Vector3 originalScale;
    private Color originalColor;

    private void Awake()
    {
        originalScale = targetNumberTxt.transform.localScale;
        originalColor = targetNumberTxt.color;
    }

    public void SetTargetNumber(int targetNumber)
    {
        targetNumberTxt.text = targetNumber.ToString();
        AnimateTargetChange(); 
    }

    private void AnimateTargetChange()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateScaleAndColor());
    }

    private IEnumerator AnimateScaleAndColor()
    {
        float timer = 0f;

        while (timer < animationDuration / 2f)
        {
            timer += Time.deltaTime;
            float t = timer / (animationDuration / 2f);

            targetNumberTxt.transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleUpFactor, t);

            targetNumberTxt.color = Color.Lerp(originalColor, highlightColor, t);

            yield return null;
        }

        timer = 0f;

        while (timer < animationDuration / 2f)
        {
            timer += Time.deltaTime;
            float t = timer / (animationDuration / 2f);

            targetNumberTxt.transform.localScale = Vector3.Lerp(originalScale * scaleUpFactor, originalScale, t);

            targetNumberTxt.color = Color.Lerp(highlightColor, originalColor, t);

            yield return null;
        }

        targetNumberTxt.transform.localScale = originalScale;
        targetNumberTxt.color = originalColor;
    }
}
