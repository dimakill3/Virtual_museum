using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menuManager { get; private set; }

    // Указатель
    [SerializeField] GameObject crosshair;

    // Корневой объект меню магазина
    [SerializeField] GameObject shop;

    // Корневой объект меню гида
    [SerializeField] GameObject guides;

    // Корневой объект меню выхода
    [SerializeField] GameObject exitMenu;

    // Событие открытия гида
    public Action<Showpiece> OnGuideOpen;
    // Событие закрытия гида
    public Action OnGuideClose;
    // Открыто ли хоть одно меню
    private bool isAnyMenuOpen = true;
    // Источник фоновой музыки
    private AudioSource backgroudMusic;
    // Список снимков
    private List<MenuSettingsMemento> mementos;

    // Задаём одиночку, фоновую музыку и инициализируем список снимков
    public void Awake()
    {
        menuManager = this;
        backgroudMusic = GetComponent<AudioSource>();
        mementos = new List<MenuSettingsMemento>();
    }

    public void SetCrosshair(GameObject crossHair)
    {
        crosshair = crossHair;
    }

    // Сохраняем опции для игры
    private void Start()
    {
        SaveOptions(new MenuSettingsMemento(false, false, 0.3f, true, false));
    }

    // Проверка нажатия клавиш изменения опций
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (exitMenu.active)
                CloseExitWindow();
            else
                OpenExitWindow();

        if (Input.GetKeyDown(KeyCode.M))
            if (backgroudMusic.mute)
                UnmuteMusic();
            else
                MuteMusic();

        if (!isAnyMenuOpen)
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (crosshair.active)
                    crosshair.SetActive(false);
                else
                    crosshair.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.T))
                ExternalData.externalData.ChangeTimeVisible();
    }

    // Открыть меню магазина
    public void OpenShop()
    {
        SaveOptions();

        shop.SetActive(true);

        OnOpenAnyMenu();

        ExternalData.externalData.ShowTime();
    }

    // Закрыть меню магазина
    public void CloseShop()
    {
        shop.SetActive(false);

        LoadOptions();
    }

    // Открыть меню гида
    public void OpenGuides(Showpiece sp)
    {
        SaveOptions();

        guides.SetActive(true);

        OnOpenAnyMenu();
        SetMusicVolume(0.1f);

        OnGuideOpen?.Invoke(sp);
    }

    // Закрыть меню гида
    public void CloseGuides()
    {
        guides.SetActive(false);

        //OnCloseShopOrGuide();

        LoadOptions();

        OnGuideClose?.Invoke();
    }

    // Открыть меню закрытия игры
    public void OpenExitWindow()
    {
        SaveOptions();

        ExternalData.externalData.enabled = false;
        exitMenu.SetActive(true);

        OnOpenAnyMenu();

        MuteMusic();
    }

    // Закрыть меню закрытия игры
    public void CloseExitWindow()
    {
        ExternalData.externalData.enabled = true;
        exitMenu.SetActive(false);

        LoadOptions();
    }

    // Выход в главное меню (для кнопки)
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    // Заглушить музыку
    public void MuteMusic()
    {
        backgroudMusic.mute = true;
    }

    // Вернуть звук музыки
    public void UnmuteMusic()
    {
        backgroudMusic.mute = false;
    }

    // Установить громкость музыки
    public void SetMusicVolume(float value = 0.3f)
    {
        backgroudMusic.volume = value;
    }

    // При открытии любого меню
    private void OnOpenAnyMenu()
    {
        isAnyMenuOpen = true;

        Customer.customer.GetComponent<CustomerMove>().enabled = false;
        Customer.customer.GetComponent<CustomerView>().enabled = false;
        Customer.customer.GetComponent<Interractive>().enabled = false;

        crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Сохранить опции игры
    public void SaveOptions(MenuSettingsMemento mem = null)
    {
        if (mem == null)
            mementos.Add(new MenuSettingsMemento(isAnyMenuOpen, backgroudMusic.mute, backgroudMusic.volume, crosshair.active, Cursor.visible));
        else
            mementos.Add(mem);
    }

    // Загрузить последний снимок опций
    private void LoadOptions(int saveNum = 0)
    {
        MenuSettingsMemento memento = mementos[mementos.Count - 1 - saveNum];

        isAnyMenuOpen = memento.IsAnyMenuOpen;

        if (isAnyMenuOpen)
        {
            Customer.customer.GetComponent<CustomerMove>().enabled = false;
            Customer.customer.GetComponent<CustomerView>().enabled = false;
            Customer.customer.GetComponent<Interractive>().enabled = false;
        }
        else 
        {
            Customer.customer.GetComponent<CustomerMove>().enabled = true;
            Customer.customer.GetComponent<CustomerView>().enabled = true;
            Customer.customer.GetComponent<Interractive>().enabled = true;
        }

        backgroudMusic.mute = memento.IsMusicMute;
        backgroudMusic.volume = memento.MusicVolume;
        crosshair.SetActive(memento.IsCrosshairActive);

        if (memento.IsCursorActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        int mementoNum = mementos.Count - 1 - saveNum;
        
        while(mementoNum < mementos.Count)
            mementos.RemoveAt(mementoNum);
    }

    public MenuSettingsMemento GetLastMemento()
    {
        if (mementos.Count == 0)
            Debug.Log("Нэма снимка");
        return mementos[mementos.Count - 1];
    }
}
