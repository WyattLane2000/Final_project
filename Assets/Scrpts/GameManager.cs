using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UiController ui;
    private float MilesTraveled = 0;
    // Start is called before the first frame update
    private bool TwoDIsPaused = false;
    private void Awake()
    {
        Messenger.AddListener(GameEvent.TWOD_PAUSED, PauseTwoDCode);
        Messenger.AddListener(GameEvent.TWOD_RESUMED, ResumeTwoDCode);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.TWOD_PAUSED, PauseTwoDCode);
        Messenger.RemoveListener(GameEvent.TWOD_RESUMED, ResumeTwoDCode);
    }

    void Start()
    {
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
}