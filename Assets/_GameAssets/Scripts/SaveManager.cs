using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private const string SaveKey = "TheCirclesRoomsSave";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        data.score =
            ScoreManager.Instance.GetScore();

        string json =
            JsonUtility.ToJson(data);

        PlayerPrefs.SetString(
            SaveKey,
            json);

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
            return;

        string json =
            PlayerPrefs.GetString(SaveKey);

        SaveData data =
            JsonUtility.FromJson<SaveData>(json);

        ScoreManager.Instance.SetScore(data.score);

        Debug.Log("Game Loaded");
    }
}