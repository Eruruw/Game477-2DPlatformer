using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject warp;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.transform.position = new Vector2(warp.transform.position.x, warp.transform.position.y);
        }
    }
}
