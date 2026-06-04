using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 500f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float gravity = 1200f;
    [SerializeField] private float groundY = -300f;

    [Header("References")]
    [SerializeField] private RectTransform playerRect;
    [SerializeField] private Image playerImage;

    private float verticalVelocity;
    private bool isGrounded = true;
    private Keyboard currentKeyboard;

    public float CurrentSpeedMultiplier { get; set; } = 1f;

    private void Update()
    {
        currentKeyboard = Keyboard.current;
        if (GameManager.Instance.currentState != GameManager.GameState.Playing)
            return;

        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector2 position = playerRect.anchoredPosition;

        if(position.x < -Screen.width / 2f + playerRect.rect.width / 2f && horizontal < 0)
        {
            horizontal = 0;
            if (!isGrounded)
            {
                RoomManager.Instance.OpenRoom1();
            }
        }
        else if(position.x > Screen.width / 2f - playerRect.rect.width / 2f && horizontal > 0)
        {
            horizontal = 0;
            if (!isGrounded)
            {
                RoomManager.Instance.OpenRainRoom();
            }
        }

        position.x += horizontal * moveSpeed * CurrentSpeedMultiplier * Time.deltaTime;

        playerRect.anchoredPosition = position;

        if (horizontal != 0)
        {
            Vector3 scale = playerRect.localScale;
            scale.x = horizontal > 0 ? 1 : -1;
            playerRect.localScale = scale;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
        }

        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;

            Vector2 pos = playerRect.anchoredPosition;
            pos.y += verticalVelocity * Time.deltaTime;

            if (pos.y <= groundY)
            {
                pos.y = groundY;
                verticalVelocity = 0;
                isGrounded = true;
            }

            playerRect.anchoredPosition = pos;
        }
    }
}