using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private float resistancePower = 1f;
    private string _name;
    private Collider fruitCollider;
    private GameObject slicedPrefab;

    private void Awake()
    {
        _name = gameObject.name.Split("(")[0];
        fruitCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.relativeVelocity.magnitude > resistancePower)
        {
            if (collision.gameObject.CompareTag("Player")) Slice();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void Slice()
    {
        fruitCollider.enabled = false;
        GameManager.Instance.IncreaseScore(points);
        slicedPrefab = Resources.Load($"Prefabs/{_name}S") as GameObject;
        GameObject slicedF = Instantiate(slicedPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        foreach (Transform child in slicedF.gameObject.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(UnityEngine.Random.onUnitSphere, ForceMode.Impulse);
        }
    }
}