using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource BgmAudiosource;
    public AudioSource SfxAudiosource;

    public AudioClip slideSound;
    public AudioClip clickSound;
    public AudioClip backGroundSound;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void playButtonClick()
    {
        SfxAudiosource.PlayOneShot(clickSound);

    }
    public void playSlide()
    {
        SfxAudiosource.PlayOneShot(slideSound);
    }
    public void SoundOnOff(bool isActive)
    {
        if (isActive)
        {
            BgmAudiosource.volume = 0.5f;
        }
        else
            BgmAudiosource.volume = 0;
    }
}
