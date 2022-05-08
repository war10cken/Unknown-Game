using UnityEngine;
using DG.Tweening;
public class Dash : MonoBehaviour
{
    public float DashVelocity = 50f;
    private int Counter = 0;
    void Update()
    {
        if (Input.GetButtonDown("Fire3") && Counter >= 4000)
        {
            GetComponent<Rigidbody>().velocity += transform.forward * DashVelocity;
            Counter = 0;
        }
        if (Counter > 4000) 
        {
            Counter = 4000;
        }
        else
        {
            Counter += (int)(1 / Time.fixedDeltaTime);
        }
        //Debug.Log(Counter);
    }
}
/*
 *     public float DashTime = 0.05f;
Vector3 direction = transform.position + transform.forward * DashSpeed;
transform.position = Vector3.Slerp(transform.position, direction, DashVelocity);
//transform.position = Vector3.Lerp(transform.position,transform.forward * DashVelocity, DashTime);
*/
//GetComponent<Rigidbody>().AddForce(transform.forward * DashVelocity);
//Vector3.Lerp(transform.position , transform.forward * DashVelocity, 0.05f);