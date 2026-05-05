using UnityEngine;

public class Core : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            GameManager.Instance.sceneChanger.GoToLevelSelect();
        }
    }
}
