using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Update()
    {
        if (SaveManager.instance != null)
        {
            currencyText.text = "Cash: " + SaveManager.instance.currency.ToString();
        }
    }
}