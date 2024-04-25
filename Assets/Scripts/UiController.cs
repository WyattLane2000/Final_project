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
    //gameOver varibles
    [SerializeField] private GameOverPopup gameOverPopup;
    [SerializeField] TextMeshProUGUI wormKillsScoreGo;
    [SerializeField] TextMeshProUGUI roboKillsScoreGo;
    [SerializeField] TextMeshProUGUI boostsScoreGo;
    [SerializeField] TextMeshProUGUI repairsScoreGo;
    //gameCleared varibles
    [SerializeField] private GameClearedPopup gameClearedPopup;
    [SerializeField] TextMeshProUGUI wormKillsScoreGc;
    [SerializeField] TextMeshProUGUI roboKillsScoreGc;
    [SerializeField] TextMeshProUGUI boostsScoreGc;
    [SerializeField] TextMeshProUGUI repairsScoreGc;
    //start Varibles
    [SerializeField] private StartPopup startPopup;
    //audio Varibles
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;

    bool TwoDIsPlaying = false;//to keep track of twoD pause state
    bool ThreeDIsPlaying = false;//to keep track of 3D pause state
    private int popupsActive = 0; //for ui control
    float healthbarMaxW; //for health update
    int roboKillCount = 0;//end game stat
    int wormKillCount = 0;//end game stat
    int boostsHit = 0;//end game stat
    int repairsHit = 0;//end game stat
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
        Messenger.AddListener(GameEvent.ROBO_ENEMY_DEAD, RoboKill);//for roboKillCount
        Messenger.AddListener(GameEvent.WORM_KILL, WormKill);//for wormKillCount
        Messenger.AddListener(GameEvent.BOOST_HIT, BoostsHit);//for boostsHit
        Messenger.AddListener(GameEvent.REPAIR, RepairsHit);// for repairsHit
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
        Messenger.RemoveListener(GameEvent.ROBO_ENEMY_DEAD, RoboKill);//for roboKillCount
        Messenger.RemoveListener(GameEvent.WORM_KILL, WormKill);//for wormKillCount
        Messenger.RemoveListener(GameEvent.BOOST_HIT, BoostsHit);//for boostsHit
        Messenger.RemoveListener(GameEvent.REPAIR, RepairsHit);// for repairsHit
    }
    // Start is called before the first frame update
    void Start()
    {
        healthbarMaxW = PHealthBar.rectTransform.rect.width;
        UpdatePlayerHealth(100);
        UpdateShipHealth(100);
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
            SoundManager.Instance.PlayMusic(gameMusic);//play aproriate music
        }
        else
        {
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
            SoundManager.Instance.PlayMusic(menuMusic);//play aproriate music
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
        float fillAmount = (float)health / 100f;
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
    //On game over show and update stats
    public void ShowGameOver()
    {
        crossHair.gameObject.SetActive(false);
        gameOverPopup.Open();
        roboKillsScoreGo.text = roboKillCount.ToString();
        wormKillsScoreGo.text = wormKillCount.ToString();
        repairsScoreGo.text = repairsHit.ToString();
        boostsScoreGo.text = boostsHit.ToString();
    }
    //On game clear show and update stats
    public void ShowGameCleared()
    {
        crossHair.gameObject.SetActive(false);
        gameClearedPopup.Open();
        roboKillsScoreGc.text = roboKillCount.ToString();
        wormKillsScoreGc.text = wormKillCount.ToString();
        repairsScoreGc.text = repairsHit.ToString();
        boostsScoreGc.text = boostsHit.ToString();
    }

    //for counting robot kills
    void RoboKill()
    {
        roboKillCount++;
    }
    //for counting worm kills
    void WormKill()
    {
        wormKillCount++;
    }
    //for counting Boosts hit
    void BoostsHit()
    {
        boostsHit++;
    }
    //for counting Repairs hit
    void RepairsHit()
    {
        repairsHit++;
    }
}
