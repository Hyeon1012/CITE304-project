using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public int curScene { get; private set; }

    public void GoToNthLevel(int n)
    {
        curScene = n + GameManager.LEVEL_SCENE_OFFSET;
        SceneManager.LoadScene(curScene);
    }



    public void GoToNextStage()
    {   
        SceneManager.LoadScene(++curScene);
    }

    public void Reset()
    {
        SceneManager.LoadScene(curScene);
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void Init()
    {   
        curScene = SceneManager.GetActiveScene().buildIndex;

        GameManager.Instance.inputManager.ResetKey += Reset;
    }


    public void LoadStage(string sceneName)//debug tool
    { 
        SceneManager.LoadScene(sceneName);
    }

}