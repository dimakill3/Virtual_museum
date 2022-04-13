using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SaveSettingsTest
    {
        [Test]
        public void SaveSettingsTestSimplePasses()
        {
            // Инициализируем menu manager
            var gameObj = new GameObject();

            gameObj.AddComponent<AudioSource>();

            var menuManager = gameObj.AddComponent<MenuManager>();
            menuManager.SetCrosshair(new GameObject());

            menuManager.Awake();

            // Сохраняем начальные настройки
            menuManager.SaveOptions();
            // Считываем начальыне настройки
            var startMemento = menuManager.GetLastMemento();
            // Изменяем некоторые настройки
            menuManager.MuteMusic();

            menuManager.SetMusicVolume(0.5f);
            // Сохраняем изменённые настройки
            menuManager.SaveOptions();
            // Считываем новые настройки
            var nextMemento = menuManager.GetLastMemento();

            // Сравниваем изменения. Некоторые настройки должны совпадать, другие - нет 
            Assert.AreEqual(startMemento.IsAnyMenuOpen, nextMemento.IsAnyMenuOpen);
            Assert.AreEqual(startMemento.IsCrosshairActive, nextMemento.IsCrosshairActive);
            Assert.AreEqual(startMemento.IsCursorActive, nextMemento.IsCursorActive);
            Assert.AreNotEqual(startMemento.IsMusicMute, nextMemento.IsMusicMute);
            Assert.AreNotEqual(startMemento.MusicVolume, nextMemento.MusicVolume);
        }
    }
}
