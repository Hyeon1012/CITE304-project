using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseAmount = 20f;
    public AudioClip noiseClip;
    public GameObject tempSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NoiseManager.Instance.AddNoise(noiseAmount);
            GameObject temp = Instantiate(tempSound, transform.parent);
            temp.GetComponent<TempSound>()?.Init(noiseClip);
            gameObject.SetActive(false);
        }
    }
}