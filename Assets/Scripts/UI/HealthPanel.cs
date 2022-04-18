using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class HealthPanel : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        public GameObject[] allHealthIcons;
        [SerializeField] private GameObject healthIconPrefab;
        
        [Inject]
        public void Construct(PlayerHealth playerHealth) => 
            this.playerHealth = playerHealth;

        void Start() => 
            playerHealth.OnHealthChanged += SetCurrentHealth;
        
        private void SetCurrentHealth()
        {
            if (allHealthIcons.Length==0)
                CreateHealthIcons();
            DisableAllIcons();
            EnableActiveIcons();
        }

        private void CreateHealthIcons()
        {
            allHealthIcons = new GameObject[playerHealth.currentHealth];
            for (int i = 0; i < playerHealth.MaxHealth; i++)
            {
                GameObject newIcon = Instantiate(healthIconPrefab, this.transform);
                allHealthIcons[i] = newIcon;
            }
        }

        private void EnableActiveIcons()
        {
            for (int i = 0; i < playerHealth.currentHealth; i++)
            {
                allHealthIcons[i].SetActive(true);
            }
        }

        private void DisableAllIcons()
        {
            for (int i = 0; i < allHealthIcons.Length; i++)
            {
                allHealthIcons[i].SetActive(false);
            }
        }
    }
}
