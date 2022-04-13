using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interractive : MonoBehaviour
{
    [SerializeField]
    Camera _camera;

    [SerializeField]
    TextMeshProUGUI interractText;

    // Уведомление и место его спавна
    [SerializeField] 
    GameObject notifycation, notifyParent;

    private Ray _ray;

    private RaycastHit _hit;

    private float _distance = 5;

    // Текущий элемент пула
    private int poolPos = 0;

    // Пул уведомлений
    private NotifyObj[] notifycationPool = new NotifyObj[3];

    private void Update()
    {
        SetRay();
        DrawRay();
    }

    private void Start()
    {
        for (int i = 0; i < notifycationPool.Length; i++)
        {
            notifycationPool[i] = Instantiate(notifycation, notifyParent.transform).GetComponent<NotifyObj>();
        }
    }

    private void OnEnable()
    {
        List<Hall> halls = Museum.museum.GetHalls();

        foreach (var hall in halls)
        {
            hall.OnStateChanged += HallStateChangedNotify;
            hall.OnPreClose += HallPreCloseNotify;
        }
    }

    private void SetRay()
    {
        _ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
    }

    private void DrawRay()
    {
        if (Physics.Raycast(_ray, out _hit, _distance) && _hit.collider.gameObject.GetComponent<Showpiece>() != null)
        {

            Showpiece sp = _hit.collider.gameObject.GetComponent<Showpiece>();

            if (!interractText.IsActive() && Customer.customer.FindAnyHallGuide(sp.GetHallID()))
                interractText.gameObject.SetActive(true);

            if (interractText.IsActive())
                if (Input.GetKey(KeyCode.F))
                {
                    interractText.gameObject.SetActive(false);

                    MenuManager.menuManager.OpenGuides(sp);
                }

            Debug.DrawRay(_ray.origin, _ray.direction * _distance, Color.blue);
        }
        else if (interractText.IsActive())
            interractText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Уведомление об изменении состояния зала
    /// </summary>
    /// <param name="hallID"> Номер зала </param>
    public void HallStateChangedNotify(int hallID)
    {
        if (this.enabled)
        {
            Hall hall = Museum.museum.GetHall(hallID + 1);

            notifycationPool[poolPos].Show($"Зал № {hallID + 1} {hall.GetState().GetDescription()}", poolPos);
            IncPoolPos();
        }
    }

    /// <summary>
    /// Уведомление перед закрытием зала
    /// </summary>
    /// <param name="hallID"> Номер зала </param>
    /// <param name="minutes"> Время до закрытия </param>
    public void HallPreCloseNotify(int hallID, int minutes)
    {
        if (this.enabled)
        {
            notifycationPool[poolPos].Show($"Зал № {hallID} закроется через {minutes} минут", poolPos);
            IncPoolPos();
        }
    }

    // Увеличить позицию в пуле на 1
    private void IncPoolPos()
    {
        poolPos = poolPos == notifycationPool.Length - 1 ? 0 : poolPos + 1;
    }

    private void OnDisable()
    {
        List<Hall> halls = Museum.museum.GetHalls();

        foreach (var hall in halls)
        {
            hall.OnStateChanged -= HallStateChangedNotify;
            hall.OnPreClose -= HallPreCloseNotify;
        }
    }
}
