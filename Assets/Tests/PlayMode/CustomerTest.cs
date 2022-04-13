using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CustomerTest
    {
        // Тест проверки покупки билета, связанный с тратой денег на него
        [UnityTest]
        public IEnumerator NormalBuyingTestMoney()
        {
            // Создаём игровой объект посетителя
            var gameObj = new GameObject();

            var customer = gameObj.AddComponent<Customer>();

            // Узнаём количество его денег
            var allMoney = customer.GetMoney();
            // Создаём билет и задаём ему значения
            var ticket = ScriptableObject.CreateInstance<Ticket>();
            // Денег хватает на покупку билета
            ticket.cost = allMoney - 100;
            ticket.hallID = 2;
            // Проверяем, купился ли билет
            Assert.IsTrue(customer.BuyTicket(ticket));

            var moneyAfterBuying = customer.GetMoney();

            yield return null;
            // Если билет купился, проверяем количество оставшихся денег
            Assert.AreEqual(moneyAfterBuying, allMoney - ticket.GetCost());
        }

        // Тест проверки покупки билета, связанный с приобритением билета посетителем
        [UnityTest]
        public IEnumerator NormalBuyingTestTicket()
        {
            // Создаём игровой объект посетителя
            var gameObj = new GameObject();

            var customer = gameObj.AddComponent<Customer>();
            // Создаём билет и задаём ему значения
            var ticket = ScriptableObject.CreateInstance<Ticket>();
            // Денег хватает на покупку билета
            ticket.cost = customer.GetMoney() - 100;
            ticket.hallID = 2;
            // Проверяем, купился ли билет
            Assert.IsTrue(customer.BuyTicket(ticket));

            yield return null;
            // Если билет купился, проверяем есть ли билет у посетителя
            Assert.IsTrue(customer.FindTicket(ticket.hallID));
        }

        // Тест проверки покупки билета, связанный с тратой денег на него (денег не хватает)
        [UnityTest]
        public IEnumerator NoEnoughtMoneyBuyingTestMoney()
        {
            // Создаём игровой объект посетителя
            var gameObj = new GameObject();

            var customer = gameObj.AddComponent<Customer>();

            var allMoney = customer.GetMoney();
            // Создаём билет и задаём ему значения
            var ticket = ScriptableObject.CreateInstance<Ticket>();
            // Денег не хватает на покупку билета
            ticket.cost = allMoney + 100;
            ticket.hallID = 2;

            // Проверяем, купился ли билет (он не должен купиться)
            Assert.IsFalse(customer.BuyTicket(ticket));

            var moneyAfterBuying = customer.GetMoney();

            yield return null;
            // Если билет не купился, проверяем потратились ли деньги
            Assert.AreEqual(moneyAfterBuying, allMoney);
        }

        // Тест проверки покупки билета, связанный с приобритением билета посетителем (денег не хватает)
        [UnityTest]
        public IEnumerator NoEnoughtMoneyBuyingTestTicket()
        {
            // Создаём игровой объект посетителя
            var gameObj = new GameObject();

            var customer = gameObj.AddComponent<Customer>();
            // Создаём билет и задаём ему значения
            var ticket = ScriptableObject.CreateInstance<Ticket>();
            // Денег не хватает на покупку билета
            ticket.cost = customer.GetMoney() + 100;
            ticket.hallID = 2;

            // Проверяем, купился ли билет (он не должен купиться)
            Assert.IsFalse(customer.BuyTicket(ticket));

            var moneyAfterBuying = customer.GetMoney();

            yield return null;
            // Если билет не купился, проверяем что билета у посетителя нет
            Assert.IsFalse(customer.FindTicket(ticket.hallID));
        }
    }
}
