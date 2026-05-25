using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] private float _minAlpha = 0.2f;
    [SerializeField] private float _maxAlpha = 1.0f;
    [SerializeField] private float _pulseSpeed = 2f;

    private Light2D _light;
    private Color _baseColor;

    void Start()
    {
        _light = GetComponent<Light2D>();
        if (_light != null)
        {
            _baseColor = _light.color;
        }
    }

    void Update()
    {
        if (_light == null) return;
        float t = (Mathf.Sin(Time.time * _pulseSpeed) + 1f) / 2f;
        float currentAlpha = Mathf.Lerp(_minAlpha, _maxAlpha, t);
        Color newColor = _baseColor;
        newColor.a = currentAlpha;
        _light.color = newColor;
    }
}