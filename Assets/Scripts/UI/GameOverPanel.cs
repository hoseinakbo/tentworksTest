using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public TextMeshProUGUI yourScoreText;
    public TextMeshProUGUI topScoreText;

    public GameObject normalTitle;
    public GameObject newHighScoreTitle;

    public void ShowPanel()
    {
        int topScore = PlayerPrefs.GetInt("topScore");
        int yourScore = GameManager.Instance.GetScore();

        if (yourScore > topScore)
        {
            PlayerPrefs.SetInt("topScore", yourScore);
            topScore = yourScore;
            newHighScoreTitle.SetActive(true);
            normalTitle.SetActive(false);
        }
        else
        {
            normalTitle.SetActive(true);
            newHighScoreTitle.SetActive(false);
        }

        yourScoreText.text = "Your Score: " + yourScore;
        topScoreText.text = "Top Score: " + topScore;

        gameObject.SetActive(true);
    }

    public void NewGameButtonOnClick()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
}
