using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShowpieceDecorator : IShowpiece
{
    protected IShowpiece wrappe;
    
    // Установить экспонат для обёртки
    public void SetWrappe(IShowpiece sp)
    {
        wrappe = sp;
    }
    // Вывод описания
    public virtual string GetDescription()
    {
        return "";
    }
}
