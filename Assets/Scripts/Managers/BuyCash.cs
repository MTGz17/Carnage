using UnityEngine;

public class BuyCash : MonoBehaviour
{
    public void AddCurrency(int amount)
    {
        SaveManager.instance?.AddCurrency(amount);
    }

    public void Add200() => AddCurrency(200);
    public void Add500() => AddCurrency(500);
    public void Add1000() => AddCurrency(1000);
}