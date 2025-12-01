using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; private set; }

    public int currentCar;
    public int currency;
    public bool[] carsUnlocked;

    public float masterVolume = 0f;
    public float musicVolume = 0f;
    public float sfxVolume = 0f;

    private string savePath;
    private int totalCars = 0; // Will be set dynamically based on CarSelector

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

        // Load saved data
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
            carsUnlocked = null; // Will be initialized later
            masterVolume = 0f;
            musicVolume = 0f;
            sfxVolume = 0f;
            Debug.Log("No save file found. Starting with default values.");
        }
    }

    /// <summary>
    /// Call this when the total number of cars is known (e.g., from CarSelector)
    /// </summary>
    public void InitializeCars(int numberOfCars)
    {
        totalCars = numberOfCars;

        if (carsUnlocked == null || carsUnlocked.Length < totalCars)
        {
            bool[] newCarsUnlocked = new bool[totalCars];
            newCarsUnlocked[0] = true; // Always unlock first car

            if (carsUnlocked != null)
            {
                for (int i = 0; i < carsUnlocked.Length; i++)
                {
                    newCarsUnlocked[i] = carsUnlocked[i];
                }
            }

            carsUnlocked = newCarsUnlocked;
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
    public bool[] carsUnlocked;
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
}