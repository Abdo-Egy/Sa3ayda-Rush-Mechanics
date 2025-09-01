using UnityEngine;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] GameObject InteractUiMessage;
    [SerializeField] TextMeshProUGUI InteractUiNameObject;
    [SerializeField] int RaycasatDistance = 10;
    [SerializeField] LayerMask InteractLayerMask;
    Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }
    private void Update()
    {
        if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit Hit, RaycasatDistance, InteractLayerMask))
        {
            Iinteractble interactble = Hit.collider.GetComponent<Iinteractble>();
            if (interactble != null) {
                InteractUiMessage.SetActive(true);
                InteractUiNameObject.text = Hit.collider.name;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactble.Interact();
                }
            }
        }
        else
        {
            InteractUiMessage.SetActive(false);
        }
    }
}
