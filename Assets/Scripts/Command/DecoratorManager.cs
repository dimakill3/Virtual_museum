using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorManager
{
    // Список декораторов
    public List<ShowpieceDecorator> decorators;

    // Инициализация мэнеджера
    public DecoratorManager()
    {
        decorators = new List<ShowpieceDecorator>();
    }

    /// <summary>
    /// Добавляет декоратор, если его ещё нет в списке, иначе удаляет его из списка
    /// </summary>
    /// <param name="spDec"></param>
    public void AddDecorator(ShowpieceDecorator spDec)
    {
        foreach (var dec in decorators)
            if (dec.GetType() == spDec.GetType())
            {
                DelConcreteDecorator(spDec.GetType());
                return;
            }

        decorators.Add(spDec);
    }

    /// <summary>
    /// Удалить конкретный декоратор из списка
    /// </summary>
    /// <param name="decType"> Тип декоратора, который нужно удалить </param>
    private void DelConcreteDecorator(Type decType)
    {
        for (int i = 0; i < decorators.Count; i++)
            if (decorators[i].GetType() == decType)
            {
                decorators.RemoveAt(i);
                Debug.Log($"Из очереди удалён декоратор типа {decType}");
                return;
            }
    }

    /// <summary>
    /// Обработать все декораторы по порядку
    /// </summary>
    /// <param name="sp"> Экспонат, который будет обёртыватсья в декораторы </param>
    /// <returns></returns>
    public string ProcessDecorators(IShowpiece sp)
    {
        if (decorators.Count == 0)
            return sp.GetDescription();

        for (int i = 0; i < decorators.Count; i++)
        {
            if (i == 0)
                decorators[i].SetWrappe(sp);
            else
                decorators[i].SetWrappe(decorators[i - 1]);
        }

        return decorators[decorators.Count - 1].GetDescription();
    }
}
