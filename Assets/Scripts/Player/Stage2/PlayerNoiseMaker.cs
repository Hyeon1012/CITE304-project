using UnityEngine;

public class PlayerNoiseMaker : MonoBehaviour
{
    public float JumpNoise = 10f;
    public float WalkNoise = 6f;

    public void MakeJumpNoise()
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(JumpNoise);
    }
    public void MakeWalkNoise()
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(WalkNoise * Time.deltaTime);
    }
}
