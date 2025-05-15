using UnityEngine;

/// <summary>
/// Detect the volume of audio
/// </summary>
public class AudioVolumeListener : MonoBehaviour
{
    public AudioSource source;


    public Vector3 minScale;
    public Vector3 maxScale;

    public MicrophoneInputController audioDetector;

    public float _currentVolume = 0f;
    private int _sampleSize = 128;

    public float volumeSensitivity = 50f;
    public float threshold = 0.1f;


    //private void Update()
    //{
    //    if (source != null && source.isPlaying)
    //    {
    //        GetMicrophoneVolume();
    //    }
    //}

    public float GetMicrophoneVolume()
    {
        return CalculateVolume(Microphone.GetPosition(Microphone.devices[0]), source.clip);
    }

    private float CalculateVolume(int clipPos, AudioClip clip)
    {
        int startPosition = clipPos - _sampleSize;
        if (startPosition < 0) { return 0; }
        float[] samples = new float[_sampleSize];
        clip.GetData(samples, startPosition);
        float totalVolume = 0f;
        for (int i = 0; i < _sampleSize; i++)
        {
            totalVolume += Mathf.Abs(samples[i]);
        }
        float normalizedVolume = Mathf.Clamp01(totalVolume / _sampleSize * volumeSensitivity);
        return normalizedVolume;

    }


}
