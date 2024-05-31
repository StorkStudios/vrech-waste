using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;

    private void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnEnable()
    {
        scoreText.text = $"Time: {Time.time}";
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
