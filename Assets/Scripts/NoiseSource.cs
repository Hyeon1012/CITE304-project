using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseAmount = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NoiseManager.Instance.AddNoise(noiseAmount);
            gameObject.SetActive(false);
        }
    }
}