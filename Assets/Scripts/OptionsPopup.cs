using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : BasePopup
{
    [SerializeField] private Slider sfxSlider;
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
    //slider setting change update volume
    public void OnSfxVolumeChanged()
    {
        SoundManager.Instance.SfxVolume = sfxSlider.value;
    }
    //slider setting change update volume
    public void OnMusicVolumeChanged()
    {
        SoundManager.Instance.MusicVolume = musicSlider.value;
    }
}
