using UnityEngine;
using TMPro;

public class RainRoomManager : MonoBehaviour
{
    public static RainRoomManager Instance;

    [Header("Timer")]
    [SerializeField] private float maxTime = 60f;
    [SerializeField] private float currentTime = 60f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    private bool timerRunning;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        currentTime -= Time.deltaTime;

        UpdateTimerUI();

        if (currentTime <= 0)
        {
            currentTime = 0;
            timerRunning = false;

            GameManager.Instance.GameOver();
        }
    }

    public void StartRainTimer()
    {
        currentTime = maxTime;
        timerRunning = true;

        UpdateTimerUI();
    }

    public void StopRainTimer()
    {
        timerRunning = false;
    }

    public void AddTime(float amount)
    {
        currentTime += amount;

        if (currentTime > maxTime)
            currentTime = maxTime;

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(currentTime).ToString();
    }
}