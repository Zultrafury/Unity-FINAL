using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private GameObject enemy;

    void Update()
    {
        if (Mathf.Abs(player.transform.position.y - transform.position.y - 5) < 4.0f)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
