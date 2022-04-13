using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    // Элемент выбора гида
    [SerializeField]  TMP_Dropdown guideType;
    // Элемент выбора зала
    [SerializeField]  TMP_Dropdown hallNumber;
    // Поле для вывода количетсва денег
    [SerializeField] TextMeshProUGUI money;
    // Событие продажи билета
    public Action<int> OnSellTicket;

    /// <summary>
    /// Продажа Билета
    /// </summary>
    private void SellTicket()
    {
        int hallID = hallNumber.value + 1;

        Customer customer = Customer.customer;

        if (customer.BuyTicket(Museum.museum.GetHall(hallID).GetTicket()))
        {
            GetComponent<Dropdowns>().SelectHall(hallNumber.value);

            money.text = customer.GetMoney().ToString();
            OnSellTicket?.Invoke(hallID);
        }
        else
            money.GetComponentInParent<Animation>().Play();
    }

    /// <summary>
    /// Продажа гида
    /// </summary>
    private void SellGuide()
    {
        int hallID = hallNumber.value + 1;

        if (Customer.customer.BuyGuide(new Guide(hallID, guideType.value == 0 ? (Tell) new AudioTell() : (Tell) new TextTell())))
        {
            GetComponent<Dropdowns>().SelectHall(hallNumber.value);

            money.text = Customer.customer.GetMoney().ToString();
        }
        else
            money.GetComponentInParent<Animation>().Play();
    }

    /// <summary>
    /// Выход в музей из меню кассы
    /// </summary>
    public void LeaveShop()
    {
        MenuManager.menuManager.CloseShop();
    }

    /// <summary>
    /// Выход в музей из меню гида
    /// </summary>
    public void LeaveGuides()
    {
        MenuManager.menuManager.CloseGuides();
    }
}
