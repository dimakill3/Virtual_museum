using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Dropdowns : MonoBehaviour
{
    // Поле для вывода описания зала
    [SerializeField] TextMeshProUGUI hallDescription;
    // Картинка для отображения превью зала
    [SerializeField] Image hallPreview;
    // Элемент выбора гида
    [SerializeField] TMP_Dropdown guideChooseDropdown;
    // Элемент выбора зала
    [SerializeField] TMP_Dropdown hallChooseDropdown;
    // Кнопка покупки билета
    [SerializeField] Button BuyTicketButton;
    // Кнопка покупки гида
    [SerializeField] Button BuyGuideButton;
    // Посетитель
    private Customer customer;
    // Текущее значение выбранного зала в дропдаун
    private int currentValue;

    // Подписываемся на изменение состояния зала для динамичного изменения описания
    // + выводим описание первого зала
    private void Start()
    {
        List<Hall> halls = Museum.museum.GetHalls();

        foreach (var hall in halls)
            hall.OnStateChanged += UpdateHallDescription;

        customer = Customer.customer;

        SelectHall(0);
        currentValue = 0;
    }

    private void OnDisable()
    {
        List<Hall> halls = Museum.museum.GetHalls();

        foreach (var hall in halls)
            hall.OnStateChanged -= UpdateHallDescription;
    }

    // Выбор другого зала в дропдаун
    public void SelectHall(int value)
    {
        int hallID = value + 1;

        Hall hall = Museum.museum.GetHall(hallID);

        #region SethallDescription

        hallDescription.text = $"Тематика: {hall.GetTheme()}\n" +
            $"Количество экспонатов: {hall.GetShowpieceCount()}\n" +
            $"Расписание: {hall.GetTimetableInString()}\n" +
            $"Состояние: {hall.GetState().GetDescription()}\n" +
            $"Цена билета: {hall.GetTicket().GetCost()}\n" +
            $"Цена текстового гида: {new TextTell().Accept(new GuideCost(), hallID)}\n" +
            $"Цена аудиогида: {new AudioTell().Accept(new GuideCost(), hallID)}";

        hallPreview.sprite = hall.GetPreview();

        #endregion

        #region WorkWithShopInterface

        BuyTicketButton.interactable = !customer.FindTicket(hallID);

        guideChooseDropdown.interactable = !BuyTicketButton.interactable;

        BuyGuideButton.interactable = !BuyTicketButton.interactable && !customer.FindGuide(hallID, guideChooseDropdown.value == 0 ? typeof(AudioTell) : typeof(TextTell));

        #endregion

        currentValue = value;
    }

    private void UpdateHallDescription(int value)
    {
        if (hallChooseDropdown.value == value)
            SelectHall(value);
    }

    // Выбор другого гида в дропдаун
    public void SelectGuide(int value)
    {
        int hallID = hallChooseDropdown.value + 1;

        BuyGuideButton.interactable = !BuyTicketButton.interactable && !customer.FindGuide(hallID, value == 0 ? typeof(AudioTell) : typeof(TextTell));
    }
}
