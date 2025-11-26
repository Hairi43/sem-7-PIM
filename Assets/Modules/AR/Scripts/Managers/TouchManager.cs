using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using TMPro;

public class TouchManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction touchPressAction;
    private InputAction touchPositionAction;

    
    [SerializeField] private TMP_Text output;

    [SerializeField] private Camera arCamera;

    private float lastTouchTime = 0f;
    [SerializeField] private float touchCooldown = 2f; // seconds

    private bool isPressed = false;


    PopupNotification _popupNotification;
    GameObject popupController;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];

        // for popups 
        popupController = GameObject.Find("PopUpController");
        _popupNotification = popupController.GetComponent<PopupNotification> ();
    }

    private void OnEnable() {
        if (playerInput == null) {
            playerInput = GetComponent<PlayerInput>();
            if (playerInput == null) {
                Debug.LogError("Brakuje komponentu PlayerInput!");
                // output.text = "Brakuje komponentu PlayerInput!";
                return;
            }
        }

        if (playerInput.actions == null) {
            Debug.LogError("PlayerInput nie ma przypisanego InputActionAsset!");
            // output.text = "PlayerInput nie ma przypisanego InputActionAsset!";
            return;
        }

        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];

        if (touchPressAction == null) {
            Debug.LogError("Nie znaleziono akcji 'TouchPress' w InputActionAsset.");
            // output.text = "Nie znaleziono akcji 'TouchPress' w InputActionAsset.";
            return;
        }

        if (popupController == null) {
            Debug.LogError("Nie znaleziono akcji 'PopupController'.");
            // output.text = "Nie znaleziono akcji 'PopupController'.";
            return;
        }

        if (_popupNotification == null) {
            Debug.LogError("Nie znaleziono akcji 'popupMessage'.");
            // output.text = "Nie znaleziono akcji 'popupMessage'.";
            return;
        }

        if (isPressed) return;

        touchPressAction.performed += OnTouchPressed;
    }


    private void OnDisable() {
        if (touchPressAction != null && isPressed)
            touchPressAction.performed -= OnTouchPressed;
    }

    private void OnTouchPressed(InputAction.CallbackContext context) {
        if (Time.time - lastTouchTime < touchCooldown) return;
        lastTouchTime = Time.time;
        TogglePressed();

        Vector2 touchPosition = touchPositionAction.ReadValue<Vector2>();
        // output.text = $"touchPosition: {touchPosition}";

        // if (IsPointerOverUI(touchPosition)) return; // Ignoruj UI

        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            Debug.Log($"Trafiono obiekt: {hit.collider.name}");
            // output.text = $"Trafiono obiekt: {hit.collider.name}";

            IShowPopups showPopUp = hit.collider.GetComponent<IShowPopups> ();
            if (showPopUp != null)
            {
                // output.text = $"przed: {hit.collider.name}";
                showPopUp.ShowPopUp();
                // output.text = $"po: {hit.collider.name}";
            }
            else
            {
                Debug.LogWarning($"Brak komponentu IShowPopups na obiekcie {hit.collider.name}");
            }
        } 
        // Ray ray = arCamera.ScreenPointToRay(touchPosition);
        // if (Physics.Raycast(ray, out RaycastHit hit, 100f)) {
        //     Debug.Log($"Hit: {hit.collider.name} on layer {hit.collider.gameObject.layer}");
        // }
        else {
            Debug.Log("Nie trafiono nic w przestrzeni 3D.");
            // output.text = "Nie trafiono nic w przestrzeni 3D.";
        }
    }


    private void TogglePressed()
    {
        isPressed ^= true;
    }

    // useless and doesn't work i guess
    private bool IsPointerOverUI(Vector2 position) {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
