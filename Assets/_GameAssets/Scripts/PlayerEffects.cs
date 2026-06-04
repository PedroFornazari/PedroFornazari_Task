using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;

    private Coroutine speedRoutine;

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedRoutine != null)
            StopCoroutine(speedRoutine);

        speedRoutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        playerController.CurrentSpeedMultiplier = multiplier;

        yield return new WaitForSeconds(duration);

        playerController.CurrentSpeedMultiplier = 1f;
    }
}