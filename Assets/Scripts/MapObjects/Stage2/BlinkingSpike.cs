using UnityEngine;
using System.Collections;

public class BlinkingSpike : MonoBehaviour
{
    public GameObject spike1;
    public GameObject spike2;

    public float toggleInterval = 2.0f;

    private void Start()
    {
        StartCoroutine(ToggleRoutine());
    }

    private IEnumerator ToggleRoutine()
    {
        while (true)
        {
            spike1.SetActive(!spike1.activeSelf);
            spike2.SetActive(!spike2.activeSelf);

            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
