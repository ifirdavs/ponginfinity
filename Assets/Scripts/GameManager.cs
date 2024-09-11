using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private int roundsToWin = 3;

    private bool isPlaying = false;
    public bool isVSPlayer = false;

    public void StartGameVSCPU()
    {
        StartGame();
    }

    public void StartGameVSPlayer()
    {
        StartGame();
        isVSPlayer = true;
    }

    public void StartGame()
    {
        UIManager.GetInstance().SwitchUI(UIType.Gameplay);
        SceneManager.LoadScene("Game");
        playerOneScore = 0;
        playerTwoScore = 0;
        isPlaying = false;
        UIManager.GetInstance().ChangeText(UIType.Gameplay, "PlayerOneScoreText", playerOneScore.ToString());
        UIManager.GetInstance().ChangeText(UIType.Gameplay, "PlayerTwoScoreText", playerTwoScore.ToString());
        UIManager.GetInstance().ActiveteObject(UIType.Gameplay, "StartRoundText", true);
    }

    public void StartRound(InputAction.CallbackContext context)
    {
        if (context.performed && !isPlaying)
        {
            isPlaying = true;
            UIManager.GetInstance().ActiveteObject(UIType.Gameplay, "StartRoundText", false);
            FindObjectOfType<BallController>().AddSpeed();
        }
    }

    public void EndRound()
    {
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1.5f);
        isPlaying = false;
        if (playerOneScore == roundsToWin)
        {
            SceneManager.LoadScene("GameOver");
            UIManager.GetInstance().ChangeText(UIType.GameOver, "WinnerText", "PLAYER 1 WIN");
            UIManager.GetInstance().SwitchUI(UIType.GameOver);
        }
        else if (playerTwoScore == roundsToWin)
        {
            SceneManager.LoadScene("GameOver");
            UIManager.GetInstance().ChangeText(UIType.GameOver, "WinnerText", "PLAYER 2 WIN");
            UIManager.GetInstance().SwitchUI(UIType.GameOver);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            UIManager.GetInstance().ActiveteObject(UIType.Gameplay, "StartRoundText", true);
        }
    }

    public void IncreasePlayerOneScore()
    {
        playerOneScore += 1;
        UIManager.GetInstance().ChangeText(UIType.Gameplay, "PlayerOneScoreText", playerOneScore.ToString());
        EndRound();
    }

    public void IncreasePlayerTwoScore()
    {
        playerTwoScore += 1;
        UIManager.GetInstance().ChangeText(UIType.Gameplay, "PlayerTwoScoreText", playerTwoScore.ToString());
        EndRound();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
          Application.Quit();
         Debug.Log("Game is exiting");
#endif 
    }
}
