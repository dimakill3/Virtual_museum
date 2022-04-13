using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettingsMemento
{
    // Открыто ли меню
    public bool IsAnyMenuOpen
    {
        get;
        private set;
    }

    // Заглушена ли музыка
    public bool IsMusicMute
    {
        get;
        private set;
    }

    // Громкость музыки
    public float MusicVolume
    {
        get;
        private set;
    }

    // Видимость указателя
    public bool IsCrosshairActive
    {
        get;
        private set;
    }

    // Видимость курсора
    public bool IsCursorActive
    {
        get;
        private set;
    }

    // Инициализация снимка
    public MenuSettingsMemento(bool _isAnyMenuOpen, bool _isMusicMute, float _musicVolume, bool _isCrosshairActive, bool _isCursorActive)
    {
        IsAnyMenuOpen = _isAnyMenuOpen;
        IsMusicMute = _isMusicMute;
        MusicVolume = _musicVolume;
        IsCrosshairActive = _isCrosshairActive;
        IsCursorActive = _isCursorActive;
    }
}
