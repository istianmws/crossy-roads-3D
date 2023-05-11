using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Slider bgmSlide;
    public Slider sfxSlide;
    private float bgmVolumeBeforeQuit;
    private float sfxVolumeBeforeQuit;
    private bool muteValue;
    public AudioSource bgmSound;
    public AudioClip dieSound;
    public AudioClip jumpSound;
    public AudioClip buttonSound;
    public AudioClip eagleSound;
    public AudioClip collectCoinSound;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        bgmSound = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
         //mengambil nilai sfx dan bgm sebelumnya dari playerprefs
        bgmVolumeBeforeQuit = PlayerPrefs.GetFloat("bgmVolume");
        sfxVolumeBeforeQuit = PlayerPrefs.GetFloat("sfxVolume");
        muteValue = PlayerPrefs.GetInt("soundMute", 0) == 1;

        //mengatur nilai sfx dan bgm sebelumnya (jika ada) ke audio manager
        if (bgmVolumeBeforeQuit >= 0f)
        {
            bgmSound.volume = bgmVolumeBeforeQuit;
        }
        if (sfxVolumeBeforeQuit >= 0f)
        {
            // sfx.volume = sfxVolumeBeforeQuit;
        }
        if (muteValue)
        {
            bgmSound.mute = muteValue;
            // sfx.mute = muteValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayJump()
    {
        bgmSound.PlayOneShot(jumpSound);
    }
    public void PlayDie()
    {
        bgmSound.PlayOneShot(dieSound);
    }
    public void PlayEagle()
    {
        bgmSound.PlayOneShot(eagleSound);
    }
    public void PlayCoin()
    {
        bgmSound.PlayOneShot(collectCoinSound);
    }
    public void PlayButton()
    {
        bgmSound.PlayOneShot(buttonSound);
    }
    public void SetMute(bool value)
    {
        bgmSound.mute = value;
        // bgmSound.mute = value;
        int muteValue = value? 1:0;
        PlayerPrefs.SetInt("soundMute", muteValue);
    }
    public void SetBgmVolume(float value)
    {
        bgmSound.volume = value;
        PlayerPrefs.SetFloat("bgmVolume", bgmSound.volume);
    }
    // public void SetSfxVolume(float value)
    // {
    //     sfx.volume = value;
    //     PlayerPrefs.SetFloat("sfxVolume", sfx.volume);
    // }
}
