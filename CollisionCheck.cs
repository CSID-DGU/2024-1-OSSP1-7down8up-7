using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OntriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
