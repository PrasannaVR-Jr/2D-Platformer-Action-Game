using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TapCounterLevel : MonoBehaviour
{
    public Text tapCountText;
    public Text timerText;
    public Button tapButton;
    public float gameDuration = 10f;
    public int tapTarget = 10;

    private float timer;
    private int tapCount;

    public GameObject winPanel;

    void Start()
    {
        tapCount = 0;
        timer = gameDuration;
        UpdateUI();

        tapButton.onClick.AddListener(OnTapButtonClicked);
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;

            if (tapCount >= tapTarget)
            {
                GameWin();
            }
        }
        else
        {
            GameLose();
        }
    }

    void OnTapButtonClicked()
    {
        if (timer > 0f)
        {
            tapCount++;
            UpdateUI();

            if (tapCount >= tapTarget)
            {
                GameWin();
            }
        }
    }

    void UpdateUI()
    {
        tapCountText.text = "Taps: " + tapCount;
        timerText.text = "Time: " + Mathf.Max(0, Mathf.Round(timer));
    }

    void GameWin()
    {
        winPanel.SetActive(true);
    }

    void GameLose()
    {
        winPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
