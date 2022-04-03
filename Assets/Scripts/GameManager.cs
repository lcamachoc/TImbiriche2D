using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _gameState;
    public bool clicked = false;
    private int redscore = 0;
    private int bluescore = 0;
    public Text red;
    public Text blue;
    public Text win;
    public GameObject marca;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _gameState = GameState.start;
    }

    public void UpdateGameState(GameState gameState)
    {
        _gameState = gameState;
    }
    public void updateClick()
    {
        clicked = !clicked;
    }
    public GameState GetGameState => _gameState;

    public void SwitchPlayer()
    {
        if (_gameState == GameState.player1)
            _gameState = GameState.player2;
        else
            _gameState = GameState.player1;
    }

    void Update()
    {
        switch (_gameState)
        {
            case GameState.start:
                UpdateGameState(GameState.player1);
                break;
            case GameState.player1:
                break;
            case GameState.player2:
                break;
            case GameState.end:
                break;
        }
    }
    public void addScore(int i, int j)
    {
        if (GameManager.Instance.GetGameState == GameManager.GameState.player2)
        {
            bluescore++;
            blue.text = "Score: " + bluescore; ;
            marca.GetComponent<SpriteRenderer>().color = Color.blue;
            Instantiate(marca, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity);
        }
        if (GameManager.Instance.GetGameState == GameManager.GameState.player1)
        {
            redscore++;
            red.text = "Score: " + redscore;
            marca.GetComponent<SpriteRenderer>().color = Color.red;
            Instantiate(marca, new Vector3(i + 0.5f, j + 0.5f, 0), Quaternion.identity);
        }
        if(redscore> ((BoardManager.Instance.Height - 1) * (BoardManager.Instance.Width - 1)) / 2)
        {
            blue.text = "";
            red.text = "";
            win.color = Color.red;
            win.text = "Red Wins";
            UpdateGameState(GameState.end);
        }
        if (bluescore > ((BoardManager.Instance.Height-1) * (BoardManager.Instance.Width-1))/2)
        {
            blue.text = "";
            red.text = "";
            win.color = Color.blue;
            win.text = "Blue Wins";
            UpdateGameState(GameState.end);
        }
    }
    public enum GameState
    {
        start,
        player1,
        player2,
        end
    }
}
