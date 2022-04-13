using System.Collections.Generic;
using UnityEngine;

public class CustomerMove : MonoBehaviour
{
    // Скорость и гравитация
    [SerializeField]
    float speed;

    private float gravity = 20;

    // Контролер для передвижения персонажа
    [SerializeField] CharacterController controller;

    [SerializeField]
    GameObject crosshair;

    private int inHall = -1;

    private Vector3 hallExit = new Vector3(0, 0, 0);

    // Место спавна персонажа
    private Vector3 spawn = new Vector3(115f, 0f, 42.5f);

    // Скрытие курсора, подписка на события, перемещение на спавн, зарузка пула
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.position = spawn;

        crosshair.SetActive(true);
    }

    private void OnEnable()
    {
        List<Hall> halls = Museum.museum.GetHalls();

        foreach (var hall in halls)
        {
            hall.OnHallClose += GoToExit;
        }
    }

    private void GoToExit(int hallID)
    {
        if (inHall == hallID)
        {
            controller.enabled = false;
            transform.position = hallExit;
            controller.enabled = true;
        }
    }

    // Вектор движения
    private Vector3 direction;

    // Реализация передвижения
    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0, moveVertical);

        float localSpeed = speed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                localSpeed *= 2;
            }

        direction = transform.TransformDirection(direction) * localSpeed;
        
        if (!controller.isGrounded)
            direction.y -= gravity;

        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
    }

    // Столкновение с кругом для магазина (открывается магазин)
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ShopCircle")
        {
            MenuManager.menuManager.OpenShop();
        }
        else if (other.tag == "HallEntry")
        {
            Hall h = other.GetComponentInParent<Hall>();
            inHall = inHall == -1 ? h.GetHallID() : -1;

            if (inHall != -1)
                hallExit = h.GetExitPosition();
        }
    }

    private void OnDisable()
    {
        List<Hall> halls = Museum.museum.GetHalls();

        foreach (var hall in halls)
        {
            hall.OnHallClose -= GoToExit;
        }
    }
}
