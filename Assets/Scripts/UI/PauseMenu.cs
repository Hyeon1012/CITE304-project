using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        GameManager.Instance.ResumeGame();
    }

    public void Title()
    {
        GameManager.Instance.sceneChanger.GoToTitle();
    }
}
