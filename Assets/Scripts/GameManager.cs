using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiController ui;
    [SerializeField] GameObject twoDFacOne;
    [SerializeField] GameObject twoDFacTwo;
    [SerializeField] GameObject twoDFacThree;
    [SerializeField] GameObject twoDCamera;
    [SerializeField] GameObject threeDCamera;
    private float MilesTraveled = 0;
    // Start is called before the first frame update
    private bool TwoDIsPaused = false;
    int roboKillCount = 0;
    int levelCount = 0;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseTwoDCode);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeTwoDCode);
        Messenger.AddListener(GameEvent.BOOST_HIT, BoostedScore);
        Messenger.AddListener(GameEvent.ROBO_ENEMY_DEAD, RoboKill);
        Messenger.AddListener(GameEvent.FAC_ONE_HIT, StartThreeD); //for swaping to 3D
        Messenger.AddListener(GameEvent.FAC_TWO_HIT, StartThreeD); //for swaping to 3D
        Messenger.AddListener(GameEvent.FAC_THREE_HIT, StartThreeD); //for swaping to 3D
        Messenger.AddListener(GameEvent.SWAP_GAME, SwapGame);//for swaping back to 2D
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseTwoDCode);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeTwoDCode);
        Messenger.RemoveListener(GameEvent.BOOST_HIT, BoostedScore);
        Messenger.RemoveListener(GameEvent.ROBO_ENEMY_DEAD, RoboKill);
        Messenger.RemoveListener(GameEvent.FAC_ONE_HIT, StartThreeD);//for swaping to 3D
        Messenger.RemoveListener(GameEvent.FAC_TWO_HIT, StartThreeD);//for swaping to 3D
        Messenger.RemoveListener(GameEvent.FAC_THREE_HIT, StartThreeD);//for swaping to 3D
        Messenger.RemoveListener(GameEvent.SWAP_GAME, SwapGame);//for swaping back to 2D
    }

    void Start()
    {
        Messenger.Broadcast(GameEvent.THREED_PAUSED);
        Messenger.Broadcast(GameEvent.TWOD_PLAYING);
        ui.ShowStartPopup();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TwoDIsPaused)//2D related code
        {
            MilesTraveled += 0.05f;
            float roundedMilesTraveled = Mathf.Round(MilesTraveled * 10f) / 10f;
            ui.UpdateScore(roundedMilesTraveled);
            if(MilesTraveled > 500 && levelCount < 1)
            {
                twoDFacOne.SetActive(true);
            }else if(MilesTraveled > 1000 && levelCount < 2)
            {
                twoDFacTwo.SetActive(true);
            }
            else if (MilesTraveled > 1500 && levelCount < 3)
            {
                twoDFacThree.SetActive(true);
            }
        }
    }

    // Method to pause 2D related code
    public void PauseTwoDCode()
    {
        TwoDIsPaused = true;
    }

    // Method to resume 2D related code
    public void ResumeTwoDCode()
    {
        TwoDIsPaused = false;
    }

    //for boosting miles traveled
    public void BoostedScore()
    {
        MilesTraveled += 50f;
        float roundedMilesTraveled = Mathf.Round(MilesTraveled * 10f) / 10f;
        ui.UpdateScore(roundedMilesTraveled);
    }

    //for counting robot kills
    void RoboKill()
    {
        roboKillCount++;
    }

    //for activating FacOne and swaping to 3D
    void StartThreeD()
    {
        Messenger.Broadcast(GameEvent.TWOD_PAUSED);
        Messenger.Broadcast(GameEvent.THREED_PLAYING);
        Messenger.Broadcast(GameEvent.THREED_RESUMED);
        twoDCamera.SetActive(false);
        threeDCamera.SetActive(true);
        levelCount++;
    }

    //for swaping back to 2D
    void SwapGame()
    {
        Messenger.Broadcast(GameEvent.THREED_PAUSED);
        Messenger.Broadcast(GameEvent.TWOD_PLAYING);
        Messenger.Broadcast(GameEvent.TWOD_RESUMED);
        twoDCamera.SetActive(true);
        threeDCamera.SetActive(false);
    }
}
