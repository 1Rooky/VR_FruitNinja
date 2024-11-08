using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1f)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GetComponent<Collider>().enabled = false;
                GameManager.Instance.Explode();
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }
}