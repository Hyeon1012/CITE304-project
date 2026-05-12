using UnityEngine;
using System.Collections;

public class NPCGhost : MonoBehaviour
{
    [Header("설정")]
    public float duration = 3.0f;       // n초 동안 이동
    public float moveDistance = 14.0f;   // 오른쪽으로 이동할 거리
    public float targetAlpha = 0.5f;    // 중간 지점에서 도달할 투명도 (0.5 = 반투명)

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(MoveAndFadeRoutine());
    }

    private IEnumerator MoveAndFadeRoutine()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.right * moveDistance;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration; // 0.0 ~ 1.0 진행도

            // 1. 오른쪽으로 이동
            transform.position = Vector3.Lerp(startPos, endPos, percent);

            // 2. 투명도 조절 (0 -> 0.5 -> 0)
            // Mathf.Sin(0) = 0, Mathf.Sin(π/2) = 1, Mathf.Sin(π) = 0 임을 이용
            float currentAlpha = Mathf.Sin(percent * Mathf.PI) * targetAlpha;

            Color color = _spriteRenderer.color;
            color.a = currentAlpha;
            _spriteRenderer.color = color;

            yield return null;
        }

        // 확실하게 투명하게 만들고 종료
        Color finalColor = _spriteRenderer.color;
        finalColor.a = 0f;
        _spriteRenderer.color = finalColor;

        // 필요하다면 이동이 끝난 후 오브젝트 파괴
        // Destroy(gameObject);
    }
}