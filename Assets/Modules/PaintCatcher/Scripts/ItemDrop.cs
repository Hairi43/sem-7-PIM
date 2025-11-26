using Modules.PaintCatcher.Scripts;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float destroyHeight = -6f;  

    void Update()
    {
        
        if (transform.position.y < destroyHeight)
        {
        
            var playerBody = FindFirstObjectByType<CartController>();
            if (playerBody != null)
            {
                playerBody.AddScore();      
            }

           
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.CompareTag("PlayerDodger")) return;

        var playerBody = other.GetComponent<CartController>();
        if (playerBody == null) return;

       
        if (playerBody.itemCatcher != null) {
            playerBody.itemCatcher.GameOver();
        }


        Destroy(gameObject);
    }
}