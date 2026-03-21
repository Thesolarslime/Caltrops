using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource Sound;

    public AudioClip[] AudioClips;

    public void Start()
    {
        Sound = GetComponent<AudioSource>();
    }
    public void PlaySound(int SoundID, bool PitchVariance, float Volume)
    {
        if (PitchVariance) { Sound.pitch = Random.Range(0.85f, 1.15f); }
        Sound.volume = Volume;
        Sound.clip = AudioClips[SoundID];
        Sound.Play();
    }
}
