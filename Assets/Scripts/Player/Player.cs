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
        void FixedUpdate()
        {
            HPtext.text = "HP " + HP;
            Staminatext.text = "Stamina " + STAMINA;
        }
        void OnCollisionStay(Collision collisioninfo)
        {
            if (collisioninfo.impulse.sqrMagnitude > 1 && collisioninfo.gameObject.GetComponent<Rigidbody>() && HP >= 0)
            {
                HP -= (int)(collisioninfo.impulse.sqrMagnitude / collisioninfo.gameObject.GetComponent<Rigidbody>().mass);
            }
            else if (HP < 0)
            {
                HP = 0;
                GAMEOVER.enabled = true;
            }
        }
    }

}
