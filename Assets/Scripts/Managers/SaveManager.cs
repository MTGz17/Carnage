using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public int currentCar;
    public int currency;
    public bool [] carsUnlocked = new bool [3] {true, false, false};

    public float masterVolume = 0f;
    public float musicVolume = 0f;
    public float sfxVolume = 0f;

    private string savePath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        Load();
    }

    #region Save/Load

    public void Save()
    {
        PlayerData data = new PlayerData
        {
            currentCar = currentCar,
            currency = currency,
            carsUnlocked = carsUnlocked,
            masterVolume = masterVolume,
            musicVolume = musicVolume,
            sfxVolume = sfxVolume
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            currentCar = data.currentCar;
            currency = data.currency;
            carsUnlocked = data.carsUnlocked;
            masterVolume = data.masterVolume;
            musicVolume = data.musicVolume;
            sfxVolume = data.sfxVolume;
        }
        else
        {
            currentCar = 0;
            currency = 0;
            if(carsUnlocked == null)
            {
                carsUnlocked = new bool [3] {true, false, false};
            }
            masterVolume = 0f;
            musicVolume = 0f;
            sfxVolume = 0f;
            Debug.Log("No save file found. Starting with default values.");
        }
    }

    #endregion

    #region Currency Management

    public void AddCurrency(int amount)
    {
        currency += amount;
        Save();
    }

    public void SetCurrency(int amount)
    {
        currency = amount;
        Save();
    }
    #endregion
}

[System.Serializable]
class PlayerData
{
    public int currentCar;
    public int currency;
    public bool [] carsUnlocked;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
}