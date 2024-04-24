using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    //private int score = 0;
    [SerializeField] private TextMeshProUGUI MilesTraveled;
    [SerializeField] private Image PHealthBar;
    [SerializeField] private TextMeshProUGUI PHealth;
    [SerializeField] private Image SHealthBar;
    [SerializeField] private TextMeshProUGUI SHealth;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    //[SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] private StartPopup startPopup;

    bool TwoDIsPlaying = false;//to keep track of twoD pause state
    bool ThreeDIsPlaying = false;//to keep track of 3D pause state
    private int popupsActive = 0;
    float healthbarMaxW;
    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.PLAYER_HEALTH_CHANGED, UpdatePlayerHealth);
        Messenger<int>.AddListener(GameEvent.SHIP_HEALTH_CHANGED, UpdateShipHealth);
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        //to keep track of game state
        Messenger.AddListener(GameEvent.TWOD_PLAYING, TwoDPlaying);
        //to keep track of game state
        Messenger.AddListener(GameEvent.THREED_PLAYING, ThreeDPlaying);
    }
    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.PLAYER_HEALTH_CHANGED, UpdatePlayerHealth);
        Messenger<int>.RemoveListener(GameEvent.SHIP_HEALTH_CHANGED, UpdateShipHealth);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        //to keep track of game state
        Messenger.RemoveListener(GameEvent.TWOD_PLAYING, TwoDPlaying);
        //to keep track of game state
        Messenger.RemoveListener(GameEvent.THREED_PLAYING, ThreeDPlaying);
    }
    // Start is called before the first frame update
    void Start()
    {
        healthbarMaxW = PHealthBar.rectTransform.rect.width;
        //UpdateScore(score);
        UpdatePlayerHealth(100);
        UpdateShipHealth(300);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && popupsActive == 0)
        {
            optionsPopup.Open();
        }
    }

    // update score display
    public void UpdateScore(float newScore)
    {
        MilesTraveled.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            //Messenger.Broadcast(GameEvent.GAME_ACTIVE);
            if (TwoDIsPlaying)
            {
                Messenger.Broadcast(GameEvent.TWOD_RESUMED);
            }
            if (ThreeDIsPlaying)
            {

                crossHair.gameObject.SetActive(true); // show the crosshair
                Messenger.Broadcast(GameEvent.THREED_RESUMED);
            }
            Time.timeScale = 1; // unpause the game
            Cursor.lockState = CursorLockMode.Locked; // lock cursor at center
            Cursor.visible = false; // hide cursor
        }
        else
        {
            //Messenger.Broadcast(GameEvent.GAME_INACTIVE);
            if (TwoDIsPlaying)
            {
                Messenger.Broadcast(GameEvent.TWOD_PAUSED);
            }
            if (ThreeDIsPlaying)
            {

                crossHair.gameObject.SetActive(false); // turn off the crosshair
                Messenger.Broadcast(GameEvent.THREED_PAUSED);
            }
            Time.timeScale = 0; // pause the game
            Cursor.lockState = CursorLockMode.None; // let cursor move freely
            Cursor.visible = true; // show the cursor
        }
    }

    private void UpdatePlayerHealth(int health)
    {
        //find percent
        float fillAmount = (float)health /100f;
        //update num
        PHealth.text = health.ToString();
        // Update health bar width
        float newWidth = healthbarMaxW * fillAmount;
        RectTransform healthBarRectTransform = PHealthBar.rectTransform;
        Vector2 sizeDelta = healthBarRectTransform.sizeDelta;
        sizeDelta.x = newWidth;
        healthBarRectTransform.sizeDelta = sizeDelta;
    }
    private void UpdateShipHealth(int health)
    {
        //find percent
        float fillAmount = (float)health / 300f;
        //update num
        SHealth.text = health.ToString();
        // Update health bar width
        float newWidth = healthbarMaxW * fillAmount;
        RectTransform healthBarRectTransform = SHealthBar.rectTransform;
        Vector2 sizeDelta = healthBarRectTransform.sizeDelta;
        sizeDelta.x = newWidth;
        healthBarRectTransform.sizeDelta = sizeDelta;
    }
    private void OnPopupOpened()
    {
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }
        popupsActive++;
    }

    private void OnPopupClosed()
    {
        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
        }
    }
    public void ShowStartPopup()
    {
        startPopup.Open();
    }
    
    //if 3D playing set bool to show it
    void ThreeDPlaying()
    {
        TwoDIsPlaying = false;
        ThreeDIsPlaying = true;
        crossHair.gameObject.SetActive(true);
    }
    //if 2D playing set bool to show it
    void TwoDPlaying()
    {
        TwoDIsPlaying = true;
        ThreeDIsPlaying = false;
        crossHair.gameObject.SetActive(false);
    }
}
