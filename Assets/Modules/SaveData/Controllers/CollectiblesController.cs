using Modules.SaveData.Services;
using Modules.SaveData.Services.Modules.SaveData.Services;
using UnityEngine;
using Firebase.Auth;

namespace Modules.SaveData.Controllers
{
    public class CollectiblesController : MonoBehaviour
    {
        public static CollectiblesController Instance { get; private set; }
        
        public CreatureService Creature { get; private set; } = new CreatureService();
        public FactsService Facts { get; private set; } = new FactsService();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
            Debug.Log("CollectiblesController.Awake() called");
        }
    }
}