using UnityEngine;
using static InventorySlot;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    [Header("Rooms")]
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject roomRain;
    [SerializeField] private GameObject roomHof;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        OpenRoom1();
    }

    public void OpenRoom1()
    {
        room1.SetActive(true);
        roomRain.SetActive(false);
        roomHof.SetActive(false);

        int blueCoins = InventoryManager.Instance.inventoryScore;

        ScoreManager.Instance.AddPoints(blueCoins);

        for (int i = 0; i < blueCoins; i++)
        {
            InventoryManager.Instance.RemoveItem(CircleType.Point);
        }
    }

    public void OpenRainRoom()
    {
        room1.SetActive(false);
        roomRain.SetActive(true);
        roomHof.SetActive(false);

        RainRoomManager.Instance.StartRainTimer();

        CircleSpawner spawner =
            FindFirstObjectByType<CircleSpawner>();

        if (spawner != null)
            spawner.ResetRain();
    }

    public void OpenHallOfFame()
    {
        room1.SetActive(false);
        roomRain.SetActive(false);
        roomHof.SetActive(true);
    }
}