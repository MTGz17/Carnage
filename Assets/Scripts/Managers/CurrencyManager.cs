using UnityEngine;
using System.IO;

[System.Serializable]
public class CurrencyData
{
    public int currency;
}

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public int Currency { get; private set; } = 0;

    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            filePath = Path.Combine(Application.persistentDataPath, "currency.json");
            LoadCurrency();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        SaveCurrency();
        Debug.Log($"Currency added. New total: {Currency}");
    }

    public void SetCurrency(int amount)
    {
        Currency = amount;
        SaveCurrency();
        Debug.Log($"Currency set to: {Currency}");
    }

    public bool SpendCurrency(int amount)
    {
        if (Currency >= amount)
        {
            Currency -= amount;
            SaveCurrency();
            Debug.Log($"Currency spent: {amount}. Remaining: {Currency}");
            return true;
        }
        else
        {
            Debug.Log("Not enough currency to spend.");
            return false;
        }
    }

    private void SaveCurrency()
    {
        CurrencyData data = new CurrencyData { currency = Currency };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log($"Currency saved to file at: {filePath}");
    }

    private void LoadCurrency()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CurrencyData data = JsonUtility.FromJson<CurrencyData>(json);
            Currency = data.currency;
            Debug.Log($"Currency loaded from file: {Currency}");
        }
        else
        {
            Currency = 0;
            Debug.Log("No currency save file found. Starting at 0.");
        }
    }
}