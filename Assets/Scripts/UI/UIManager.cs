using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }
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

    public FridgePanel fridgePanel;
    public GameOverPanel gameOverPanel;
    public GameObject pausePanel;
    public TextMeshProUGUI toastMessageText;
    public GameObject inGameUI;
    public Image playerIngredientImage;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI topScoreText;
    public GameObject[] toDeactivateOnGameOver;

    [Space()]
    public float toastMessageTime = 2f;
    public float scoreMessageTime = 2f;

    [Space()]
    public Sprite vegetableSprite;
    public Sprite choppedVegetableSprite;
    public Sprite meatSprite;
    public Sprite cookedMeatSprite;
    public Sprite cheeseSprite;

    public bool hasActivePanel = false;

    void Start()
    {
        fridgePanel.ClosePanel();
        pausePanel.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        toastMessageText.transform.parent.gameObject.SetActive(false);
        topScoreText.text = "Top Score: " + PlayerPrefs.GetInt("topScore").ToString();

        hasActivePanel = false;
    }

    void Update()
    {
        if (GameManager.Instance.isGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pausePanel.activeSelf)
                    ResumeButtonOnClick();
                else
                    PauseButtonOnClick();
            }
        }
    }

    public void PauseButtonOnClick()
    {
        fridgePanel.ClosePanel();

        Time.timeScale = 0;
        pausePanel.SetActive(true);
        hasActivePanel = true;
    }
    public void ResumeButtonOnClick()
    {
        pausePanel.SetActive(false);
        hasActivePanel = false;
        Time.timeScale = 1;
    }
    public void ReturnToMenuOnClick()
    {
        SceneManager.LoadSceneAsync("MenuScene");
    }
    public void QuitGameOnClick()
    {
        Application.Quit();
    }

    public void ShowToastMessage(string message)
    {
        StopCoroutine("HideToastMessage");

        toastMessageText.text = message;
        toastMessageText.transform.parent.gameObject.SetActive(true);

        StartCoroutine("HideToastMessage");
    }
    IEnumerator HideToastMessage()
    {
        yield return new WaitForSeconds(toastMessageTime);

        toastMessageText.transform.parent.gameObject.SetActive(false);
    }
    public void ShowScoreMessage(TextMeshProUGUI windowScoreText, int score)
    {
        string message = score.ToString();
        if (score > 0)
            message = "+" + score;

        windowScoreText.text = message;
        windowScoreText.gameObject.SetActive(true);

        StartCoroutine(HideScoreMessage(windowScoreText));
    }
    IEnumerator HideScoreMessage(TextMeshProUGUI windowScoreText)
    {
        yield return new WaitForSeconds(scoreMessageTime);

        windowScoreText.gameObject.SetActive(false);
    }
    public void ShowGameOverPanel()
    {
        for (int i = 0; i < toDeactivateOnGameOver.Length; i++)
        {
            toDeactivateOnGameOver[i].SetActive(false);
        }

        gameOverPanel.ShowPanel();
    }

    public void UpdateTimerUI(float newTime)
    {
        TimeSpan time = TimeSpan.FromSeconds(newTime);

        timerText.text = time.ToString(@"m\:ss");
    }
    public void UpdateScoreUI(float newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdatePlayerIngredientUI()
    {
        UpdateIngredientUI(playerIngredientImage, GameManager.Instance.playerInventory.equippedIngredient);
    }

    public void UpdateIngredientUI(Image ingredientImage, Ingredient ingredient)
    {

        switch (ingredient)
        {
            case Ingredient.None:
                ingredientImage.gameObject.SetActive(false);
                break;
            case Ingredient.Vegetable:
                ingredientImage.sprite = vegetableSprite;
                ingredientImage.gameObject.SetActive(true);
                break;
            case Ingredient.ChoppedVegetable:
                ingredientImage.sprite = choppedVegetableSprite;
                ingredientImage.gameObject.SetActive(true);
                break;
            case Ingredient.Meat:
                ingredientImage.sprite = meatSprite;
                ingredientImage.gameObject.SetActive(true);
                break;
            case Ingredient.CookedMeat:
                ingredientImage.sprite = cookedMeatSprite;
                ingredientImage.gameObject.SetActive(true);
                break;
            case Ingredient.Cheese:
                ingredientImage.sprite = cheeseSprite;
                ingredientImage.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

}
