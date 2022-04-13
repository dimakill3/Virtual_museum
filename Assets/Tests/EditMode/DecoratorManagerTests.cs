using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DecoratorManagerTests
    {
        // Тест обработки декораторов ответственным менеджером
        [Test]
        public void DecoratorManagerProcessTest()
        {
            // Создаём экспонат
            var sp = new Showpiece();
            // Задаём ему описание
            sp.SetDescription("Test");
            // Создаём декораторы, которые добавляют экспонату некоторое дополнительное описание
            var artistLincDec = new ArtistLinkDecorator();
            var spLincDec = new ShowpieceLinkDecorator();

            // Упаковываем экспонат в один декоратор
            artistLincDec.SetWrappe(sp);
            // А этот декоратор в другой декоратор (таким образом выведется описание экспоната с описанием всех декораторов)
            spLincDec.SetWrappe(artistLincDec);

            // Создаём менеджера декораторов, который должен проделать те же действия
            var decManager = new DecoratorManager();

            // Добавляем в него последовательность декораторов
            decManager.AddDecorator(new ArtistLinkDecorator());
            decManager.AddDecorator(new ShowpieceLinkDecorator());

            // Запускаем обработку декораторов менеджером (выводит описание экспоната сописаниями всех декораторов)
            // и выводим описание декоратора, который мы сделали самостоятельно
            Assert.AreEqual(decManager.ProcessDecorators(sp), spLincDec.GetDescription());
        }

        // Тест правильности добавления декораторов ответственным менеджером
        [Test]
        public void DecoratorManagerAddingDecoratorsTest()
        {
            // Создаём менеджера декораторов
            var decManager = new DecoratorManager();
            // Создаём декораторы
            var artistLincDec = new ArtistLinkDecorator();
            var spLincDec = new ShowpieceLinkDecorator();

            // При добавлении в обработчик декораторов уже существующего декоратора, он удаляется
            decManager.AddDecorator(spLincDec);
            decManager.AddDecorator(spLincDec);
            // Проверяем это
            Assert.IsEmpty(decManager.decorators);
            // Заносим в менеджера все декораторы
            decManager.AddDecorator(artistLincDec);
            decManager.AddDecorator(spLincDec);

            // Затем создаём свой список декораторов
            List<ShowpieceDecorator> decList = new List<ShowpieceDecorator>();
            decList.Add(artistLincDec);
            decList.Add(spLincDec);
            // Проверяем их на совпадение
            Assert.AreEqual(decList, decManager.decorators);
        }
    }
}
