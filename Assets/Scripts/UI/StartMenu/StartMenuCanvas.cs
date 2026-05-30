using UnityEngine;

public class StartMenuCanvas : MonoBehaviour
{
    public void GameStart()
    {
        GameManager.Instance.sceneChanger.GoToLevelSelect();
    }

    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }
}
