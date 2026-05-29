using UnityEngine;

public class PlayerNoiseMaker : MonoBehaviour
{
    public float JumpNoise = 10f;
    public float WalkNoise = 5f;
    public float fallNoiseWeight = 5f;
    public float threshold = 11.1f;

    /*public void MakeJumpNoise(bool shiftKey, float shiftRate)
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(shiftKey ? JumpNoise * shiftRate : JumpNoise);
    }*/

    public void MakeWalkNoise(bool shiftKey, float shiftRate)
    {
        if (NoiseManager.Instance != null) NoiseManager.Instance.AddNoise(shiftKey ? WalkNoise * shiftRate * Time.deltaTime : WalkNoise * Time.deltaTime);
    }
    public void MakeLandingNoise(float fallDistance)
    {
        Debug.Log(fallDistance);
        if (NoiseManager.Instance != null)
        {
            if (fallDistance > threshold)
            {
                NoiseManager.Instance.AddNoise(fallDistance * fallNoiseWeight);
            }
            else
            {
                NoiseManager.Instance.AddNoise(fallDistance * fallNoiseWeight * 0.5f);
            }
        }
    }
}
