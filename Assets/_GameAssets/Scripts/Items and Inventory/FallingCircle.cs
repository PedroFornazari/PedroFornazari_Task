using UnityEngine;

public class FallingCircle : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float fallSpeed = 250f;

    [Header("Ground")]
    [SerializeField] private float destroyY = -500f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        Vector2 pos = rectTransform.anchoredPosition;

        pos.y -= fallSpeed * Time.deltaTime;

        rectTransform.anchoredPosition = pos;

        if (pos.y <= destroyY)
            Destroy(gameObject);
    }
}