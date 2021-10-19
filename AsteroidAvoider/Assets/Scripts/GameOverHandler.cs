using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpouner asteroidSpouner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;



    public void EndGame()
    {
        asteroidSpouner.enabled = false;
        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Your Score: {finalScore}";
        gameOverDisplay.gameObject.SetActive(true);
    }

    public void PlasyAgain()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMainenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this);

        continueButton.interactable = false;
    }

    public void ContinueGame()
    {
        scoreSystem.StartTimer();

        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        asteroidSpouner.enabled = true;

        gameOverDisplay.gameObject.SetActive(false);
    }


}
