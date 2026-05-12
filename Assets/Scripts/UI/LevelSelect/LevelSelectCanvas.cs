using UnityEngine;

public class LevelSelectCanvas : MonoBehaviour
{
    public void Back()
    {
        GameManager.Instance.sceneChanger.GoToTitle();
    }

    public void GoToNthLevel(int level)
    {
        GameManager.Instance.sceneChanger.GoToNthLevel(level);
    }
}
