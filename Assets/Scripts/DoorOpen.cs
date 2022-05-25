using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject Card;
    public Animator GateAnimator;
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Card"))
        {
            GateAnimator.SetBool("IsTriggered",true);
        }
    }
}
