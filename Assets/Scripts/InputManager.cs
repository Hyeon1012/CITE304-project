using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public event Action<float> HorizontalKey;

    // A&D key event - [A = -1.0, D = 1.0, Both or None = 0.0]
    // Only Invoked when key is changed
    public event Action JumpKey;

    // S key event
    public event Action DownJumpKey;

    // Click key event
    public event Action Click;

    // R key event
    public event Action ResetKey;

    // esc key event
    public event Action PauseKey;

    // Click event
    public Vector2 mouse_p;

    // Tracks mouse position in unity world coordinates (not in screen pixels)
    private float lastinput_h = 0;

    public void GetInput()
    {
        if (GameManager.Instance.IsInputBlocked) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseKey?.Invoke();
        }

        if (GameManager.Instance.IsPaused) return;

        mouse_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float thisinput_h = Input.GetAxisRaw("Horizontal");
        if (lastinput_h != thisinput_h)
        {
            HorizontalKey?.Invoke(thisinput_h);
            lastinput_h = thisinput_h;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Click?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            DownJumpKey?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpKey?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetKey?.Invoke();
        }
    }
}