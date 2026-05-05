using UnityEngine;

public class StartMenuCanvas : MonoBehaviour
{
    public void GameStart()
    {
        GameManager.Instance.sceneChanger.GoToIntroduction();
    }

    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }
}
