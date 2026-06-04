using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [Header("Collection")]
    [SerializeField] private float collectDistance = 75f;

    private RectTransform playerRect;

    private void Awake()
    {
        playerRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        CheckCollection();
    }

    private void CheckCollection()
    {
        CircleItem[] circles = FindObjectsByType<CircleItem>(FindObjectsSortMode.None);

        foreach (CircleItem circle in circles)
        {
            RectTransform circleRect = circle.GetComponent<RectTransform>();

            float dx = Mathf.Abs(playerRect.position.x - circleRect.position.x);
            float dy = Mathf.Abs(playerRect.position.y - circleRect.position.y);

            if (dx < 100f && dy < 100f)
            {
                circle.enabled = false;
                Debug.Log("COLETOU!");
                bool added = InventoryManager.Instance.AddItem(circle.circleType);

                Destroy(circle.gameObject);
            }
        }
    }
}