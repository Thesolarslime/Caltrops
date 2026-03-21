using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource Sound;

    public AudioClip[] AudioClips;

    public void Start()
    {
        Sound = GetComponent<AudioSource>();
    }
    public void PlaySound(int SoundID, bool PitchVariance)
    {
        if (PitchVariance) { Sound.pitch = Random.Range(0.9f, 1.1f); }
        Sound.clip = AudioClips[SoundID];
        Sound.Play();
    }
}
