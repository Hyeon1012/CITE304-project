using TMPro;
using UnityEngine;

public class DeathPopUp : MonoBehaviour
{
    public TextMeshProUGUI gradeText;

    public string[] grade =
    {
        "NotGraded", "A+", "A0", "A-", "B+", "B0", "B-",
        "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q",
        "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

    public void OnEnable()
    {
        gradeText.text = grade[GameManager.Instance.grades[GameManager.Instance.sceneChanger.curScene - GameManager.LEVEL_SCENE_OFFSET]];
    }

    public void Retry()
    {
        GameManager.Instance.sceneChanger.Reset();
    }
}
