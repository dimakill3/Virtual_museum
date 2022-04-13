using UnityEngine;
using TMPro;

public class NotifyObj : MonoBehaviour
{
    /// <summary>
    /// Отображает уведомление
    /// </summary>
    /// <param name="text"> Сообщение уведомления </param>
    /// <param name="pos"> Номер объекта в пуле для корректного размещения уведомлений </param>
    public void Show(string text, int pos)
    {
        transform.localPosition = new Vector3(0, pos * 55, 0);
        GetComponentInChildren<TextMeshProUGUI>().text = text;
        GetComponent<Animation>().Play();
    }
}
