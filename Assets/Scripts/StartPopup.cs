using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPopup : BasePopup
{
    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }
    public void OnStartGameButton()
    {
        Debug.Log("Start Game");
        Close();
    }
}
