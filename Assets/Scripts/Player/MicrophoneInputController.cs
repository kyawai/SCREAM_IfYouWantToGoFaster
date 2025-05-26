using UnityEngine;

/// <summary>
/// Set up the ability to use microphone
/// </summary>
[RequireComponent (typeof(AudioSource))]
public class MicrophoneInputController : MonoBehaviour
{
    private AudioClip microphoneClip;
    private string _microphone;
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        //Check if any microphones are connected
        if(Microphone.devices.Length == 0 ) { Debug.LogError("NO MICROPHONES CONNECTED"); return; }

        //Use the first microphone (THIS WILL CHANGE WHEN SETTINGS ARE IN)
        _microphone = Microphone.devices[0];

        //Configure audio source
        _audioSource.loop = true;
        _audioSource.mute = true;

        StartMicrophoneRecording();
    }


    private void StartMicrophoneRecording()
    {
        //Start the recording (10 seconds)
        microphoneClip = Microphone.Start(_microphone, true, 10, AudioSettings.outputSampleRate);
        //Allow microphone to start
        while (!(Microphone.GetPosition(_microphone) > 0)) { }
        //Assign microphone clip to audio source
        _audioSource.clip = microphoneClip;
        _audioSource.Play();
        Debug.Log("microphone started");
    }

    private void StopMicrophoneRecording()
    {
        Microphone.End(_microphone);
        _audioSource.Stop();
        Debug.Log("microphone stopped");
    }

    public AudioSource GetMicrophoneAudioSource()
    {
        return _audioSource;
    }

    private void OnDisable()
    {
        StopMicrophoneRecording();
    }
}
