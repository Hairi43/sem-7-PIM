using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.MAP.Scripts {
    public class Location : MonoBehaviour, IShowPopups {
        [SerializeField] 
        private PopupMessageMAP popupMessage;

        [SerializeField] 
        private PopupMessageMAP popupController;

        [SerializeField]
        [TextArea] private string description;

        [SerializeField]
        private Sprite popupSprite;
        [SerializeField] 
        private SpriteRenderer pinSprite;

        private void Start() {
            pinSprite.sprite = popupSprite;
        }

        //TODO - Too tired to change this in editor but this shouldn't exist
        public void Show()     {
            ShowPopUp();
        }

        public void ShowPopUp() {
            if (popupController != null && popupMessage != null)
            {
                popupMessage.OpenMAP(popupSprite.name, description);
            }
            else 
            {
                Debug.Log("Problem w cube0. Wartość null");
            }
        }
    }
}