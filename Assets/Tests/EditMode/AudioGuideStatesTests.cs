using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AudioGuideStatesTests
    {
        // Проверка правильности переходов аудиогида из состояния "Воспроизведение"
        [Test]
        public void AudioGuidePlayStateTest()
        {
            // Создаём аудиогида
            var audioGuide = new AudioTell();
            // Изменяем состояние на "Воспроизведение"
            audioGuide.ChangeState(new PlayState());
            // Переходим в состояние "Воспроизведение". Состояние не должно измениться
            Assert.AreEqual(audioGuide.Play(), new PlayState().Run());

            audioGuide.ChangeState(new PlayState());
            // Переходим в состояние "Остановлен". Состояние должно измениться на "Остановлен"
            Assert.AreEqual(audioGuide.Stop(), new StoppedState().Run());

            audioGuide.ChangeState(new PlayState());
            // Переходим в состояние "Приостановлен". Состояние должно измениться на "Приостановлен"
            Assert.AreEqual(audioGuide.Pause(), new PausedState().Run());
        }

        // Проверка правильности переходов аудиогида из состояния "Приостановлен"
        [Test]
        public void AudioGuidePausedStateTest()
        {
            // Создаём аудиогида
            var audioGuide = new AudioTell();
            // Изменяем состояние на "Приостановлен"
            audioGuide.ChangeState(new PausedState());
            // Переходим в состояние "Воспроизведение". Состояние должно измениться на "Воспроизведение"
            Assert.AreEqual(audioGuide.Play(), new PlayState().Run());

            audioGuide.ChangeState(new PausedState());
            // Переходим в состояние "Остановлен". Состояние должно измениться на "Остановлен"
            Assert.AreEqual(audioGuide.Stop(), new StoppedState().Run());

            audioGuide.ChangeState(new PausedState());
            // Переходим в состояние "Приостановлен". Состояние не должно измениться
            Assert.AreEqual(audioGuide.Pause(), new PausedState().Run());
        }

        // Проверка правильности переходов аудиогида из состояния "Остановлен"
        [Test]
        public void AudioGuideStoppedStateTest()
        {
            // Создаём аудиогида
            var audioGuide = new AudioTell();
            // Изменяем состояние на "Остановлен"
            audioGuide.ChangeState(new StoppedState());
            // Переходим в состояние "Воспроизведение". Состояние должно измениться на "Воспроизведение"
            Assert.AreEqual(audioGuide.Play(), new PlayState().Run());

            audioGuide.ChangeState(new StoppedState());
            // Переходим в состояние "Остановлен". Состояние не должно измениться
            Assert.AreEqual(audioGuide.Stop(), new StoppedState().Run());

            audioGuide.ChangeState(new StoppedState());
            // Переходим в состояние "Приостановлен". Состояние не должно измениться (переход невозможен)
            Assert.AreEqual(audioGuide.Pause(), new StoppedState().Run());
        }
    }
}
