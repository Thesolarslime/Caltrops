using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -1);

        float XDis = Player.transform.position.x - transform.position.x;
        float YDis = Player.transform.position.y - transform.position.y;

        transform.position = new Vector3(transform.position.x + (XDis * 0.15f), transform.position.y + (YDis * 0.15f), -1);
    }
}
