using UnityEngine;
using System.Collections;

public class ClockTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float duration = 3.0f;
    [SerializeField] private GameObject objectToDestroy;
    [SerializeField] private float shakeMagnitude = 0.1f;

    [Header("Gimmick Settings")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private Vector3 pressedScale = new Vector3(0.9f, 0.9f, 1f);

    private SpriteRenderer spriteRenderer;
    private int clickCount = 0;
    private bool isBroken = false;
    private bool isRunning = false;
    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;

        // Ensure we start with the right look
        if (normalSprite != null)
            spriteRenderer.sprite = normalSprite;
    }

    /// <summary>
    /// Call this from an external event or script to trigger the countdown.
    /// </summary>
    public void StartTimer()
    {
        if (isBroken || isRunning) return;
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        isRunning = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // If the user clicks it 3 times while it's shaking, we kill the process
            if (isBroken) yield break;

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return to original spot before ending
        transform.localPosition = originalPosition;
        TimeOver();
    }

    private void TimeOver()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            Debug.Log("ClockTimer: Time is up. Object removed.");
        }
        isRunning = false;
    }

    private void OnMouseDown()
    {
        // Don't do anything if it's already dead
        if (isBroken) return;

        clickCount++;
        StartCoroutine(ClickEffect());

        if (clickCount >= 3)
        {
            BreakTimer();
        }
    }

    private IEnumerator ClickEffect()
    {
        transform.localScale = pressedScale;
        yield return new WaitForSeconds(0.1f);

        // Only return to normal scale if we didn't just break it
        if (!isBroken) transform.localScale = originalScale;
    }

    private void BreakTimer()
    {
        isBroken = true;
        isRunning = false;

        // This stops the shaking Coroutine immediately
        StopAllCoroutines();

        // Visual Reset
        transform.localPosition = originalPosition;
        transform.localScale = originalScale;

        if (brokenSprite != null)
            spriteRenderer.sprite = brokenSprite;

        Debug.Log("ClockTimer: Hidden gimmick triggered. Device is now useless.");
    }
}