using UnityEngine;
using UnityEngine.UI;

namespace my_Scrips
{
    public class EnemyHealthBar : MonoBehaviour
    {

        public Slider slider;

        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            slider.value = (float)currentHealth / maxHealth;
        }
        void Update()
        {
        
        }
    }
}
