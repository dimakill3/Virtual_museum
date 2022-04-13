using UnityEngine;
using TMPro;

public class ExternalData : MonoBehaviour
{
    // Поле для вывода количества денег
    public TextMeshProUGUI money;

    // Поле для вывода время
    [SerializeField] TextMeshProUGUI time;

    [SerializeField] GameObject timeText;

    public static ExternalData externalData { get; private set; }

    // Секунды в игре
    private int timerSec = 0;
    // Минуты в игре
    [SerializeField] int timerMinute;
    // Часы в игре
    [SerializeField] int timerHour;
    // Изменение времени (deltatime)
    private float secGameTime = 0;
    // Вывод времени в формате 00h:00m
    private string gameTime
    {
        get 
        {
            return ((timerHour / 10 < 1) ? "0" + timerHour.ToString() : timerHour.ToString()) + ":" + ((timerMinute / 10 < 1) ? "0" + timerMinute.ToString() : timerMinute.ToString());
        }
    }

    public void Awake()
    {
        externalData = this;
    }

    // Выводим деньги 
    private void Start()
    {
        money.text = Customer.customer.GetMoney().ToString();
    }

    // Подсчитываем время в игре
    private void Update()
    {
        secGameTime += Time.deltaTime * 1000;
        if (secGameTime >= 1)
        {
            timerSec += 1;
            secGameTime = 0;
        }

        if (timerSec >= 60)
        {
            timerMinute += 1;
            timerSec = 0;
        }

        if (timerMinute >= 60)
        {
            timerHour += 1;
            timerMinute = 0;
        }

        if (timerHour >= 24)
        {
            timerHour = 0;
        }
        time.text = gameTime;
    }

    /// <summary>
    /// Возвращает текущее время в минутах
    /// </summary>
    /// <returns></returns>
    public int GetTimeInMinutes()
    {
        return timerHour * 60 + timerMinute;
    }

    // Отобразить/спрятать время
    public void ChangeTimeVisible()
    {
        if (timeText.active)
            HideTime();
        else
            ShowTime();
    }

    public void ShowTime()
    {
        timeText.SetActive(true);
    }

    private void HideTime()
    {
        timeText.SetActive(false);
    }
}
