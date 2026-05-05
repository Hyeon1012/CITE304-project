using UnityEngine;
using System.Collections;

public class FakeHideBox1 : MonoBehaviour
{

    [SerializeField] private GameObject _boxIsFullUI;
    [SerializeField] private float waitTime = 1f;
    IEnumerator PopUp()
    {
        _boxIsFullUI.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _boxIsFullUI.SetActive(false);
    }

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(PopUp());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
