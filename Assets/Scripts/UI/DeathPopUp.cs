using TMPro;
using UnityEngine;

public class DeathPopUp : MonoBehaviour
{
    public TextMeshProUGUI gradeText;

    // NEW: Drag your new text element into this slot in the Unity Inspector
    public TextMeshProUGUI customDeathMessageText;

    public string[] grade =
    {
        "NotGraded", "A+", "A0", "A-", "B+", "B0", "B-",
        "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q",
        "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

    public void OnEnable()
    {
        gradeText.text = grade[GameManager.Instance.grades[GameManager.Instance.sceneChanger.curScene - GameManager.LEVEL_SCENE_OFFSET]];

        // NEW: Read the death cause from the GameManager and set the text
        SetDeathMessage(GameManager.Instance.lastDeathCause);
    }

    private void SetDeathMessage(string cause)
    {
        // If you don't have a UI element assigned, skip to prevent errors
        if (customDeathMessageText == null) return;

        if (cause == "clockBridge")
        {
            customDeathMessageText.text = "The stopwatch looks so... clickable!";
        }
        else if (cause == "flameGoal")
        {
            customDeathMessageText.text = "That was hot! ...Maybe you need water?";
        }
        else if (cause == "placeholder1")
        {
            customDeathMessageText.text = "Placeholder 1 Message! If you see this, this is an error :)";
        }
        else
        {
            // Default message if they die to something normal or if "" is passed
            customDeathMessageText.text = "Try Again!";
        }
    }

    public void Retry()
    {
        Debug.Log("Retry");
        GameManager.Instance.sceneChanger.Reset();
    }
}