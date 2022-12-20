using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public PlayerInventory playerInventory;

    [Header("Times")]
    public float gameTime = 180f;
    public float newOrderWaitTime = 5f;

    public float vegetableChoppingTime = 2f;
    public float meatCookingTime = 6f;

    [Header("Scores")]
    public int vegetableScore = 20;
    public int meatScore = 30;
    public int cheeseScore = 10;

    [HideInInspector]
    public bool isGameRunning;

    float timeRemaining;
    int currentScore;

    void Start()
    {
        isGameRunning = true;
        timeRemaining = gameTime;
        currentScore = 0;
        UIManager.Instance.UpdateTimerUI(timeRemaining);
        UIManager.Instance.UpdateScoreUI(currentScore);
        Time.timeScale = 1;
    }
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if(timeRemaining < 0)
        {
            timeRemaining = 0;
            Time.timeScale = 0;
            isGameRunning = false;
            UIManager.Instance.ShowGameOverPanel();
        }

        UIManager.Instance.UpdateTimerUI(timeRemaining);
    }

    #region Score Management

    public void AddScore(int score)
    {
        currentScore += score;
        UIManager.Instance.UpdateScoreUI(currentScore);
    }
    public int GetScore()
    {
        return currentScore;
    }

    #endregion


}
