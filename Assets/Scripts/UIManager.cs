using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    // MAIN MENU
    [SerializeField]
    Text HighScore;
    [SerializeField]
    GameObject MainMenu;
    [SerializeField]
    GameObject Main;
    [SerializeField]
    GameObject Difficult;


    // INGAME
    [SerializeField]
    Text IngameScore;
    [SerializeField]
    GameObject IngameUI;
    [SerializeField]
    GameObject Reload;
    [SerializeField]
    GameObject GotHit;
    [SerializeField]
    GameObject Bulet;
    [SerializeField]
    GameObject Health;

    [SerializeField]
    GameObject Round;
    
    [SerializeField]
    GameObject PauseGame;
    [SerializeField]
    GameObject LoseGame;

    private GameObject Player;
    Player playerScripts;
    private int health;
    private int buletLeft;
    private int buletMax;


    private int highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        ChangeHighScoreDisplay();
        DontDestroyOnLoad(this);
    }
    public void ChangeHighScoreDisplay()
    {
        HighScore.text = highScore.ToString();
    }
    
    public void UpdateHighScore(int newScore)
    {
        
        if(newScore <= highScore)
        {
            ChangeHighScoreDisplay();
            return;
        }

        if(newScore > highScore)
        {
            highScore = newScore;
            PlayerPrefs.SetInt("HIGH_SCORE", highScore);
            ChangeHighScoreDisplay();

        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    

    public void StartGameInit()
    {
        ChangeIngameScore(0);
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScripts = Player.GetComponent<Player>();
        health = Player.GetComponent<Health>().maxHealth;
        buletMax = playerScripts.bulletAmount;
        buletLeft = buletMax;
        ChangeBuletLeftText();
        ChangeHealth(health);  
    }

    // Hien thi mau
    public void ChangeHealth(int newAmount)
    {
        health = newAmount;
        Health.GetComponentInChildren<Text>().text = health.ToString();
    }

    #region Hien Thi dan
    // format
    public void ChangeBuletLeftText()
    {
        Bulet.GetComponentInChildren<Text>().text = buletLeft.ToString() + "/" + buletMax.ToString();
    }
    public void ChangeBuletLeftText(int buletLeft, int maxBulet)
    {
        Bulet.GetComponentInChildren<Text>().text = buletLeft.ToString() + "/" + maxBulet.ToString();
    }
    public void ChangeBulet(int newAmount)
    {
        buletLeft = newAmount;
        ChangeBuletLeftText();
    }

    public void ResetBulet(int newAmount)
    {
        buletLeft = buletMax;
        ChangeBuletLeftText();
    }

    public void ActiveReloadMessage()
    {
        Reload.SetActive(true);
    }
    public void DeActiveReloadMessage()
    {
        Reload.SetActive(false);
    }
    #endregion


    #region InGAMEUI
    public void ChangeIngameScore(int newScore)
    {
        IngameScore.text = "Score: " + newScore.ToString();
    }
    // bi danh
    public void GotHitEffect(float attackDelay)
    {
        GotHit.SetActive(true);
        CancelInvoke();
        Invoke(nameof(DeActiveGotHit), attackDelay);
    }

    public void DeActiveGotHit()
    {
        GotHit.SetActive(false);
    }

    // Change round/ round message
    public void ShowMessageRound(int round)
    {
        Round.SetActive(true);

        Round.GetComponentInChildren<Text>().text = "Round " + round.ToString();
        Invoke(nameof(HideMessageRound), 2f);
    }

    public void HideMessageRound()
    {
        Round.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        PauseGame.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        PauseGame.SetActive(false);
    }

    public void ShowLoseMenu()
    {
        LoseGame.SetActive(true);
    }

    public void CloseLoseMenu()
    {
        LoseGame.SetActive(false);
    }

    #region button controller

    // PauseMenu
    public void HandleResumeBtn()
    {
        AudioManager.Play(AudioName.ButtonClick);
        ClosePauseMenu();
        Player.GetComponent<CharController>().ResumeGame();
    }
    public void HandleMenuBtnPause()
    {
        AudioManager.Play(AudioName.ButtonClick);
        ClosePauseMenu();
        Player.GetComponent<CharController>().ResumeGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainTmp");
        ShowMainMenu();
    }
    // Lose
    public void HandleResartBtn()
    {
        AudioManager.Play(AudioName.ButtonClick);
        CloseLoseMenu();
        SceneManager.LoadScene("GamePlay");
    }
    public void HandleMenuBtnLose()
    {
        AudioManager.Play(AudioName.ButtonClick);
        CloseLoseMenu();
        Player.GetComponent<CharController>().ResumeGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainTmp");
        ShowMainMenu();
    }

    #endregion

    #endregion


    #region MAIN MENU
    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        OnInitMainMenu();
        IngameUI.SetActive(false);
    }
    public void OnInitMainMenu()
    {
        Main.SetActive(true);
        Difficult.SetActive(false);
    }
    // Main
    public void HandlePlayButtonMain()
    {
        AudioManager.Play(AudioName.ButtonClick);
        Difficult.SetActive(true);
        Main.SetActive(false);
    }
    public void HandleExitButtonMain()
    {
        AudioManager.Play(AudioName.ButtonClick);
        Application.Quit();
    }
    //Difficlut
    public void HandleDiff1()
    {
        AudioManager.Play(AudioName.ButtonClick);
        SceneManager.LoadScene("GamePlay");
        Config.Difficult = 1;
        IngameUI.SetActive(true);
        MainMenu.SetActive(false);
        
    }
    public void HandleDiff2()
    {
        AudioManager.Play(AudioName.ButtonClick);
        SceneManager.LoadScene("GamePlay");
        Config.Difficult = 2;
        IngameUI.SetActive(true);
        MainMenu.SetActive(false);

    }
    public void HandleDiff3()
    {
        AudioManager.Play(AudioName.ButtonClick);
        SceneManager.LoadScene("GamePlay");
        Config.Difficult = 3;
        IngameUI.SetActive(true);
        MainMenu.SetActive(false);

    }
    public void HandleMenuDifficult()
    {
        AudioManager.Play(AudioName.ButtonClick);
        Main.SetActive(true);
        Difficult.SetActive(false);
    }

    #endregion
}