using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [Header("Raycast")]
    public float RayLenght = 100;
    
    RaycastHit hit;
    
    [Header("Player")]
    public GameObject Player;
    
    [Header("Velocities")]
    public float PlayerSpeed = 0.2f;
    
    public void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 target;
        
        if (Physics.Raycast(ray.origin, ray.direction * RayLenght, out hit))
        {
            target = (hit.point - Player.transform.position);
            target = new Vector3(target.x, 0, target.z);
            Player.transform.forward = Vector3.Slerp(Player.transform.forward, target, PlayerSpeed);
        }
        else
        {
            target = ray.direction;
            target = new Vector3(target.x, 0, target.z);
            Player.transform.forward = Vector3.Slerp(Player.transform.forward, target, PlayerSpeed);
        }
        
        Debug.DrawRay(ray.origin, ray.direction * RayLenght, Color.cyan);
    }
}