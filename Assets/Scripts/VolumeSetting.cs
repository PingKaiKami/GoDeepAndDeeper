using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public GameObject pauseMenu;

    void Start(){
        if(PlayerPrefs.HasKey("masterVolume") || PlayerPrefs.HasKey("bgmVolume") || PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadVolume();
            pauseMenu.SetActive(false);
        }
        SetMasterVolume();
        SetBGMVolume();
        SetSFXVolume();
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetBGMVolume(){
        float volume = bgmSlider.value;
        audioMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }
    public void SetSFXVolume(){
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
