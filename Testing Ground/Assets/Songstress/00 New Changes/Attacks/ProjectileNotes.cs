using UnityEngine;

public class ProjectileNotes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private static Transform noteTransform;
    private float trackSpeedX = 1.5f;
    public static void Create(GameObject Notes, Vector3 spawnPoint, Vector3 enemyLocation)
    {
        GameObject noteFab = poolManager.SpawnObject(Notes, spawnPoint, Quaternion.identity, poolManager.PoolType.GameObject);
        ProjectileNotes projectileNotes = noteFab.GetComponent<ProjectileNotes>();
        projectileNotes.Setup(enemyLocation);

    }
    private Vector3 Enemy;
    private void Setup(Vector3 enemyLocation)
    {
        this.Enemy = enemyLocation;
    }

    private void Update()
    {
        if (Enemy != null)
        {
           
            Vector2 TrackPos = (Enemy - transform.position).normalized;
            transform.position = Vector2.MoveTowards(noteTransform.position, Enemy, Time.deltaTime * trackSpeedX);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RadWorm")
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            Debug.Log("Collided with RadWorm");
        }
    }
}
