using Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Slider _health;

        [SerializeField] private Canvas _ui;
        [SerializeField] private Canvas _gameOverScreen;

        public void TakeDamage(float damage)
        {
            _health.value -= damage / 1000f;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.DealDamage(this);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.DealDamage(this);
            }
        }

        private void Update()
        {
            if (_health.value <= 0)
            {
                Time.timeScale = 0;
                _ui.gameObject.SetActive(false);
                _gameOverScreen.gameObject.SetActive(true);
            }
        }
    }

}
