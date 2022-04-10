using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        int HP = 100;
        int STAMINA = 100;
        public Text HPtext;
        public Text Staminatext;
        public Text GAMEOVER;

        private void FixedUpdate()
        {
            HPtext.text = "HP " + HP;
            Staminatext.text = "Stamina " + STAMINA;
        }

        private void OnCollisionStay(Collision collisionInfo) 
        {
            if (collisionInfo.impulse.sqrMagnitude > 1 && collisionInfo.gameObject.GetComponent<Rigidbody>() && HP >= 0) 
            {
                HP -= (int)(collisionInfo.impulse.sqrMagnitude / collisionInfo.gameObject.GetComponent<Rigidbody>().mass);
            }
        
            if (HP < 0)
            {
                HP = 0;
                GAMEOVER.gameObject.SetActive(true);
            }
        }
    }
}
