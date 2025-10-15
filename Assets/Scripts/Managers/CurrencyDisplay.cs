using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Update()
    {
        if (CurrencyManager.Instance != null)
        {
            currencyText.text = "Cash: " + CurrencyManager.Instance.Currency.ToString();
        }
    }
}