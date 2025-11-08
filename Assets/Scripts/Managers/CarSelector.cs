using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelector : MonoBehaviour
{
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [SerializeField] private Button playButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private int[] carPrices;
    private int currentCar;

    private void Start()
    {
        currentCar = SaveManager.instance.currentCar;
        SelectCar(currentCar);
    }

    private void SelectCar(int _index)
    {
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount-1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (SaveManager.instance.carsUnlocked[currentCar])
        {
            playButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            priceText.text = carPrices[currentCar].ToString();

            buyButton.interactable = (SaveManager.instance.currency >= carPrices[currentCar]);
        }
    }

    public void ChangeCar(int _change)
    {
        currentCar += _change;

        if (currentCar > transform.childCount - 1)
        {
            currentCar = 0;
        }
        else if (currentCar < 0)
        {
            currentCar = transform.childCount - 1;
        }

        SaveManager.instance.currentCar = currentCar;
        SaveManager.instance.Save();
        SelectCar(currentCar);
    }

    public void BuyCar()
    {
        SaveManager.instance.currency -= carPrices[currentCar];
        SaveManager.instance.carsUnlocked[currentCar] = true;
        SaveManager.instance.Save();
        UpdateUI();
    }
}