using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody2D player;

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player's position with a z offset
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
