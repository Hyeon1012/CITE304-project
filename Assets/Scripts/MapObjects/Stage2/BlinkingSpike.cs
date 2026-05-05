using UnityEngine;
using System.Collections;

public class BlinkingSpike : MonoBehaviour
{
    public GameObject spike1;
    public GameObject spike2;

    public bool initial = false;
    public float toggleInterval = 2.0f;
    public float waitInterval = 1.0f;

    private bool _temp;

    private void Start()
    {
        _temp = !initial;
        StartCoroutine(ToggleRoutine());
    }

    private IEnumerator ToggleRoutine()
    {
        while (true)
        {
            spike1.SetActive(!_temp);
            spike2.SetActive(!_temp);
            _temp = !_temp;

            yield return new WaitForSeconds(toggleInterval);

            spike1.SetActive(false);
            spike2.SetActive(false);

            yield return new WaitForSeconds(waitInterval);
        }
    }
}
