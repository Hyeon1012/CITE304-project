using UnityEngine;
using UnityEngine.Events;

public class NoiseManager : MonoBehaviour
{
    public static NoiseManager Instance;

    [Header("Settings")]
    public float maxNoise = 100f;
    public float thresholdNoise = 80f;
    [SerializeField] private float _currentNoise = 0f;
    [SerializeField] private float _noiseDecayScala = 5f;
    [SerializeField] private float _noiseDecayRatio = 0.8f;
    [SerializeField] private float _chaserPeriod = 1.0f;

    private float timer = 0f;

    [Header("Events")]
    public UnityEvent OnNoiseThresholdReached;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (_currentNoise > 0)
        {
            if (_currentNoise > 50f) _currentNoise *= Mathf.Exp(Mathf.Log(_noiseDecayRatio) * Time.deltaTime);
            else _currentNoise -= _noiseDecayScala * Time.deltaTime;
            _currentNoise = Mathf.Clamp(_currentNoise, 0, maxNoise);
        }
        timer -= Time.deltaTime;
    }

    public float GetCurrentNoise()
    {
        return _currentNoise;
    }

    public void AddNoise(float amount)
    {
        _currentNoise += amount;
        _currentNoise = Mathf.Clamp(_currentNoise, 0, maxNoise);

        if (_currentNoise >= thresholdNoise)
        {
            if (timer <= 0f)
            {
                OnNoiseThresholdReached?.Invoke();
                timer = _chaserPeriod;
            }
        }
    }
}
