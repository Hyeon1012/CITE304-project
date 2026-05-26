using System.Collections.Generic;
using UnityEngine;

public class Captcha : MonoBehaviour
{
    [SerializeField] private List<int> _correctAnswers; // Dogs
    [SerializeField] private Trap2 _trapManager;
    private HashSet<int> _selectedIndices = new HashSet<int>();

    public void ToggleSelection(int index, bool isSelected)
    {
        if (isSelected)
        {
            _selectedIndices.Add(index);
        }
        else
        {
            _selectedIndices.Remove(index);
        }
    }

    public void VerifyCaptcha()
    {
        bool isPassed = true;
        if (_selectedIndices.Count != _correctAnswers.Count)
        {
            isPassed = false;
        }
        else
        {
            foreach (int ans in _correctAnswers)
            {
                if (!_selectedIndices.Contains(ans))
                {
                    isPassed = false;
                    break;
                }
            }
        }

        if (isPassed)
        {
            Debug.Log("Ä¸Ã­ Åë°ú");
            if (_trapManager != null)
            {
                _trapManager.OnQuizCorrect();
            }
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Ä¸Ã­ ½ÇÆÐ");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerStateManager stateManager = player.GetComponent<PlayerStateManager>();
                if (stateManager != null)
                {
                    stateManager.KillPlayer();
                }
            }
        }
    }
}