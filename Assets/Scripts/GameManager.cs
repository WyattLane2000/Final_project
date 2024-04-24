using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiController ui;
    private float MilesTraveled = 0;
    // Start is called before the first frame update
    private bool TwoDIsPaused = false;
    int roboKillCount = 0;
    int cyrstalsCount = 0;
     private void Awake()
    {
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseTwoDCode);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeTwoDCode);
        Messenger.AddListener(GameEvent.BOOST_HIT, BoostedScore);
        Messenger.AddListener(GameEvent.ROBO_ENEMY_DEAD, RoboKill);
        Messenger.AddListener(GameEvent.FAC_ONE_COLLECTED, CollectablesFound);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseTwoDCode);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeTwoDCode);
        Messenger.RemoveListener(GameEvent.BOOST_HIT, BoostedScore);
        Messenger.RemoveListener(GameEvent.ROBO_ENEMY_DEAD, RoboKill);
        Messenger.RemoveListener(GameEvent.FAC_ONE_COLLECTED, CollectablesFound);
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
        if (!TwoDIsPaused)
        {
            MilesTraveled += 0.05f;
            float roundedMilesTraveled = Mathf.Round(MilesTraveled * 10f) / 10f;
            ui.UpdateScore(roundedMilesTraveled);
        }
    }

    // Method to pause 
    public void PauseTwoDCode()
    {
        TwoDIsPaused = true;
    }

    // Method to resume
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

    //for counting cyrstals found
    void CollectablesFound()
    {
        cyrstalsCount++;
    }
}
