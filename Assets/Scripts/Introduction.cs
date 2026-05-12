using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

[Serializable]
public class IntroStage
{
    public Sprite stageImage;
    [TextArea(3, 10)] public string stageText;
    public bool changeImage;
}

public class Introduction : MonoBehaviour
{
    [SerializeField] private IntroStage[] introStages;
    [SerializeField] private Image targetImage;
    [SerializeField] private TMP_Text targetText;

    private int currentIndex = 0;

    void Start()
    {
        UpdateUI(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AdvanceStage();
        }
    }

    void AdvanceStage()
    {
        currentIndex++;
        if (currentIndex < introStages.Length)
        {
            UpdateUI(currentIndex);
        }
        else
        {
            GameManager.Instance.sceneChanger.GoToLevelSelect();
        }
    }

    void UpdateUI(int index)
    {
        targetText.text = introStages[index].stageText;
        if (introStages[index].changeImage && introStages[index].stageImage != null)
        {
            targetImage.sprite = introStages[index].stageImage;
        }
    }
}