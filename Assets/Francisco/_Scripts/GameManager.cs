using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;


    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private int seconds;
    private int minutes = 30;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Inventário")]
    public List<GameObject> listInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        seconds = 0;
        timer.text = "30:00";
        StartCoroutine(CountSeconds());
    }


    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenuScreen:
                break;
            case GameState.InGame:
                break;
            case GameState.EndGame:
                FadeIn();
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void FadeIn() => StartCoroutine(Fade(1, 0));
    public void FadeOut() => StartCoroutine(Fade(0, 1));

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        Color color = fadeImage.color;
        float timer = 0f;

        while (timer <= fadeDuration)
        {
            float t = timer / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }

    IEnumerator CountSeconds()
    {
        while (true)
        {
            seconds--;

            if (seconds >= 10)
            {
                timer.text = $"{minutes} : {seconds}";
            }
            else
            {
                timer.text = $"{minutes} : 0{seconds}";
                if (seconds <= 0)
                {
                    seconds = 59;
                    minutes--;
                    timer.text = $"{minutes} : {seconds}";
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    /*IEnumerator CountSeconds()
    {
        float time = 10;
        while (time >= 0)
        {
            print("time");
            time -= Time.deltaTime;
            timer.text = time.ToString();
        }
        yield return null;
    }*/

    /*private void EndCredits()
    {
        if (State == GameState.EndCredits)
        {
            UpdateGameState(GameState.MainMenuScreen);
            MenuManager.Instance.UpdateMenuState(MenuState.InitialScreen);
        }
    }*/
}

public enum GameState
{
    MainMenuScreen,
    InGame,
    EndGame
}
