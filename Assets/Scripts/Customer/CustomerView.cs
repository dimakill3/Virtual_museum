using UnityEngine;

public class CustomerView : MonoBehaviour
{
    // Чувствительность мыши
    [SerializeField]
    float sensetive = 1;

    // Скрываем курсор
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Реализация поворота
    void Update()
    {
        float viewVertical = Input.GetAxis("Mouse Y") * sensetive;
        float viewHorizontal = Input.GetAxis("Mouse X") * sensetive;

        Vector3 characterRotate = transform.rotation.eulerAngles;

        characterRotate.x = 0;
        characterRotate.y += viewHorizontal;
        characterRotate.z = 0;

        transform.rotation = Quaternion.Euler(characterRotate);

        Vector3 cameraRotate = GetComponentInChildren<Camera>().transform.rotation.eulerAngles;

        cameraRotate.x -= viewVertical;
        cameraRotate.y = characterRotate.y;
        cameraRotate.z = 0;

        GetComponentInChildren<Camera>().transform.rotation = Quaternion.Euler(cameraRotate);
    }
}
