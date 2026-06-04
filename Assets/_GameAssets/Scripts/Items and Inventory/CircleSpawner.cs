using UnityEngine;
using System.Collections;

public class CircleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject pointCircle;
    [SerializeField] private GameObject healthCircle;
    [SerializeField] private GameObject speedCircle;
    [SerializeField] private GameObject specialCircle;

    [Header("Spawn")]
    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private Transform circleContainer;

    [SerializeField] private float spawnInterval = 0.5f;

    [Header("Special")]
    [SerializeField] private bool specialCircleActive;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.currentState ==
                GameManager.GameState.Playing)
            {
                SpawnCircle();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void ResetRain()
    {
        foreach (Transform child in circleContainer)
        {
            Destroy(child.gameObject);
        }

        specialCircleActive = false;
    }

    private void SpawnCircle()
    {
        int random = Random.Range(0, 20);

        GameObject prefab = pointCircle;

        /*if (random == 0)
        {
            prefab = healthCircle;
        }
        else if (random == 1)
        {
            prefab = speedCircle;
        }
        else
        {
            prefab = pointCircle;
        }*/

        if (!specialCircleActive && Random.Range(0, 100) < 2)
        {
            prefab = specialCircle;
            specialCircleActive = true;
        }

        GameObject spawned =
            Instantiate(prefab, circleContainer);

        RectTransform rect =
            spawned.GetComponent<RectTransform>();

        float width = spawnArea.rect.width;

        float randomX =
            Random.Range(-width / 2f, width / 2f);

        rect.anchoredPosition =
            new Vector2(randomX,
                        spawnArea.anchoredPosition.y);
    }

    public void SpecialCircleCollected()
    {
        specialCircleActive = false;
    }
}