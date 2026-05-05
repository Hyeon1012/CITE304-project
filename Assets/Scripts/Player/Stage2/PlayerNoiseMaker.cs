using UnityEngine;

public class PlayerNoiseMaker : MonoBehaviour
{
    public float JumpNoise = 10f;
    public float WalkNoise = 6f;
    public float fallNoiseWeight = 5f;

    public void MakeJumpNoise(bool shiftKey, float shiftRate)
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(shiftKey ? JumpNoise * shiftRate : JumpNoise);
    }
    public void MakeWalkNoise(bool shiftKey, float shiftRate)
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(shiftKey ? WalkNoise * shiftRate * Time.deltaTime : WalkNoise * Time.deltaTime);
    }
    public void MakeLandingNoise(float fallDistance)
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(fallDistance * fallNoiseWeight);
    }
}
