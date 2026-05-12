using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{ 
    [SerializeField] private Button[] stageButtons;
    [SerializeField] private GameObject[] lockImages;

    void Start()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (i + 1 > GameManager.Instance.levelReached)
            {
                stageButtons[i].interactable = false;
                if (lockImages.Length > i && lockImages[i] != null)
                {
                    lockImages[i].SetActive(true);
                }
            }
            else
            {
                stageButtons[i].interactable = true;
                if (lockImages.Length > i && lockImages[i] != null)
                {
                    lockImages[i].SetActive(false);
                }
            }
        }
    }
}