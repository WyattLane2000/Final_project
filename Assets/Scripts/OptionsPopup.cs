using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : BasePopup
{
    [SerializeField] private TextMeshProUGUI sfxLabel;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI musicLabel;
    [SerializeField] private Slider musicSlider;
    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }
    public void OnReturnToGameButton()
    {
        Debug.Log("return to game");
        Close();
    }
    public void UpdateSfxVolume(float value)
    {
        sfxLabel.text = "SFX Volume: " + ((int)value).ToString();
    }
    //slider setting change update volume
    public void OnSfxVolumeChanged()
    {
        UpdateSfxVolume(sfxSlider.value);
        SoundManager.Instance.SfxVolume = sfxSlider.value;
    }
    public void UpdateMusicVolume(float value)
    {
        musicLabel.text = "Music Volume: " + ((int)value).ToString();
    }
    //slider setting change update volume
    public void OnMusicVolumeChanged()
    {
        UpdateMusicVolume(musicSlider.value);
        SoundManager.Instance.MusicVolume = musicSlider.value;
    }
}
