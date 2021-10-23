using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<BallManager>().GameOver();
    }
}
