using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayAudioOneShot(AudioClip audio, float volumeScale)
    {
        try
        {
            audioSource.PlayOneShot(audio, volumeScale);
        } catch(System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void PlayAudioOneShot(AudioClip audio)
    {
        try
        {
            audioSource.PlayOneShot(audio);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
}
