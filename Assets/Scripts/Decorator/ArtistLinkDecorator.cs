using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistLinkDecorator : ShowpieceDecorator
{
    // Вывести описание экспоната с ссылкой на экспонат
    public override string GetDescription()
    {
        return wrappe.GetDescription() + "\nСсылка на создателя: <ссылка>";
    }
}
