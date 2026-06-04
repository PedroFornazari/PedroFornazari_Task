using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    [Header("Current State")]
    public GameState currentState = GameState.MainMenu;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SetState(GameState.MainMenu);
    }

    private void Update()
    {
        //Keyboard currentKeyboard = Keyboard.current;
        if (currentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.P))
                PauseGame();
        }
    }   

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1f;
                UIManager.Instance.ShowMainMenu();
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                UIManager.Instance.ShowPauseMenu();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                UIManager.Instance.ShowGameOver();
                break;
        }
    }

    public void StartNewGame()
    {
        Time.timeScale = 1f;

        ScoreManager.Instance.ResetScore();

        UIManager.Instance.HideAllMenus();

        SetState(GameState.Playing);
    }

    public void LoadLastSave()
    {
        Time.timeScale = 1f;

        SaveManager.Instance.LoadGame();

        UIManager.Instance.HideAllMenus();

        SetState(GameState.Playing);
    }

    public void PauseGame()
    {
        if (currentState == GameState.Playing)
            SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            Time.timeScale = 1f;
            UIManager.Instance.HidePauseMenu();
            SetState(GameState.Playing);
        }
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;

        ScoreManager.Instance.ResetScore();

        InventoryManager.Instance.items.Clear();
        InventoryManager.Instance.inventoryScore = 0;
        FindFirstObjectByType<InventoryUI>()?.Refresh();

        RoomManager.Instance.OpenRoom1();

        UIManager.Instance.HideAllMenus();

        SetState(GameState.Playing);
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        ScoreManager.Instance.ResetScore();

        InventoryManager.Instance.items.Clear();
        InventoryManager.Instance.inventoryScore = 0;

        FindFirstObjectByType<InventoryUI>()?.Refresh();

        RoomManager.Instance.OpenRoom1();

        SetState(GameState.MainMenu);
    }

    public void BackToMenuNoRestart()
    {
        SetState(GameState.MainMenu);
    }
}