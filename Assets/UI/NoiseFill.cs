using UnityEngine;
using UnityEngine.UI;

public class NoiseFill : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    private float _maxNoise;

    void Start()
    {
        _maxNoise = NoiseManager.Instance.maxNoise;

        if (fillImage != null)
        {
            UpdateFillImage();
        }
    }

    private void Update()
    {
        if (fillImage != null)
        {
            UpdateFillImage();
        }
    }

    private void UpdateFillImage()
    {
        fillImage.fillAmount = NoiseManager.Instance.GetCurrentNoise() / _maxNoise;
    }
}
