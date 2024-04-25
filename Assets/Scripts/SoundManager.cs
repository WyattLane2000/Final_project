using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioMixer mixer;

    static public SoundManager Instance { get; private set; } = null;

    float sfxVolume = 1.0f;
    float musicVolume = 1.0f;

    private float LinearToLog(float value)
    {
        return Mathf.Log10(value) * 20;
    }
    public float SfxVolume { 
        get { return sfxVolume; } 
        set {  
            sfxVolume = Mathf.Clamp(value, 0.0f, 1.0f);
            mixer.SetFloat("SfxVolume", LinearToLog(sfxVolume));
        }
    }
    public float MusicVolume
    {
        get { return musicVolume; }
        set { 
            musicVolume = Mathf.Clamp(value, 0.0f, 1.0f);
            mixer.SetFloat("MusicVolume", LinearToLog(musicVolume));
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    const string PP_MUSIC_VOL = "MusicVol";
    const string PP_SFX_VOL = "SfxVol";

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat(PP_MUSIC_VOL, musicVolume);
        PlayerPrefs.SetFloat(PP_SFX_VOL, sfxVolume);
    }

    private void Init()
    {
        MusicVolume = PlayerPrefs.GetFloat(PP_MUSIC_VOL, 1f);
        SfxVolume = PlayerPrefs.GetFloat(PP_SFX_VOL, 1f);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.volume = musicVolume;
        musicSource.Play();

    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
