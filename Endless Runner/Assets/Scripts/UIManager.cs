using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    public List<GameObject> HUDPanels = new List<GameObject>();

    public GameObject GUIPanel;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI runnerSpeed;
    public TextMeshProUGUI obstacleFreq;

    public bool paused;
    public bool gameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        UpdateUIValues();
    }

    public void TogglePause()
    {
        if (gameOver) return;

        PlayerInput input = FindObjectOfType<PlayerInput>();

        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0f;
            input.SwitchCurrentActionMap("UI");
            HUDPanels[0].SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            input.SwitchCurrentActionMap("Player");
            HUDPanels[0].SetActive(false);
        }
    }

    public void UpdateUIValues()
    {
        playerScore.text = $"Player Score: {LevelManager.instance.playerScore.ToString()}";
        runnerSpeed.text = $"Runner Speed: {LevelManager.instance.tileSpeed.ToString()}";
        obstacleFreq.text = $"Obstacle Frequency Perc: {LevelManager.instance.obstaclePercentage.ToString()}";
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverScore.text = $"Your Score: {LevelManager.instance.playerScore}";
        GUIPanel.SetActive(false);
        HUDPanels[1].SetActive(true);
    }
}
