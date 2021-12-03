using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public Slider mVolume;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("MusicVolume"))
        {
            mVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            mVolume.value = 1f;
        }
    }

    void Update()
    {
        music.volume = mVolume.value;
    }

    // Update is called once per frame
    public void SetAudioVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", music.volume);
    }
}
