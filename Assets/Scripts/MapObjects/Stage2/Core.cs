using UnityEngine;

public class Core : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            GameManager.Instance.levelReached = GameManager.Instance.levelReached < 3 ? 3 : GameManager.Instance.levelReached;
            GameManager.Instance.sceneChanger.GoToLevelSelect();
        }
    }
}
