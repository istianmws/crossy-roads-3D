using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    
    static AudioSource bgmInstance;
    static AudioSource sfxInstance;
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;
    public bool IsMute{get=> bgm.mute;}
    public float BgmVolume{get=> bgm.volume;}
    public float SfxVolume{get=> sfx.volume;}
    private float bgmVolumeBeforeQuit;
    private float sfxVolumeBeforeQuit;
    private bool muteValue;

    private void Start() {
        if(bgmInstance != null)
        {
            Destroy(bgm.gameObject);
            bgm = bgmInstance;
        }else
        {
            bgmInstance=bgm;
            bgm.transform.SetParent(null);
            DontDestroyOnLoad(bgm.gameObject);

        }
        
        if(sfxInstance != null)
        {
            Destroy(sfx.gameObject);
            sfx= sfxInstance;
        }else
        {
            sfxInstance=sfx;
            sfx.transform.SetParent(null);
            DontDestroyOnLoad(sfx.gameObject);
        }

        //mengambil nilai sfx dan bgm sebelumnya dari playerprefs
        bgmVolumeBeforeQuit = PlayerPrefs.GetFloat("bgmVolume");
        sfxVolumeBeforeQuit = PlayerPrefs.GetFloat("sfxVolume");
        muteValue = PlayerPrefs.GetInt("soundMute", 0) == 1;

        //mengatur nilai sfx dan bgm sebelumnya (jika ada) ke audio manager
        if (bgmVolumeBeforeQuit >= 0f)
        {
            bgm.volume = bgmVolumeBeforeQuit;
        }
        if (sfxVolumeBeforeQuit >= 0f)
        {
            sfx.volume = sfxVolumeBeforeQuit;
        }
        if (muteValue)
        {
            bgm.mute = muteValue;
            sfx.mute = muteValue;
        }
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgm.isPlaying)
            bgm.Stop();  
        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfx.isPlaying)
            sfx.Stop(); 
        sfx.clip  = clip;
        sfx.Play();
        Debug.Log("lompat");
    }

    public void SetMute(bool value)
    {
        bgm.mute = value;
        sfx.mute = value;
        int muteValue = value? 1:0;
        PlayerPrefs.SetInt("soundMute", muteValue);
    }
    public void SetBgmVolume(float value)
    {
        bgm.volume = value;
        PlayerPrefs.SetFloat("bgmVolume", bgm.volume);
    }
    public void SetSfxVolume(float value)
    {
        sfx.volume = value;
        PlayerPrefs.SetFloat("sfxVolume", sfx.volume);
    }

}
