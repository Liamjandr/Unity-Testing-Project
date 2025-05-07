using Unity.VisualScripting;
using UnityEngine;

public class trackingEnemy : MonoBehaviour
{
    [SerializeField] private float trackSpeed = 10f;
    [SerializeField] private float trackingRange = 15f;

    private GameObject targetEnemy;
    private Transform noteTransform;

    //[SerializeField] private float idleAmplitude = 0.3f;
    //[SerializeField] private float idleFrequency = 1.5f;

    private float despawner = 0f;
    private float despawnTime = 5f;
    // Random idle movement
    //private Vector2 idleDirection;
    //private float idleOffset;
    //[SerializeField] private float waveAmplitude = 0.5f;
    //[SerializeField] private float waveFrequency = 5f;

    //private float travelDistance;
    //private float waveOffset; // Randomized phase offset
    //private Vector2 lastDirection; // For stable wave orientation
    void Start()
    {
        noteTransform = transform;
        //waveAmplitude += Random.Range(-0.2f, 0.2f);
        //waveFrequency += Random.Range(-1f, 1f);
        //waveOffset = Random.Range(0f, Mathf.PI * 2);

        //idleDirection = Random.insideUnitCircle.normalized;
        //idleOffset = Random.Range(0f, Mathf.PI * 2);
    }

    void Update()
    {
        targetEnemy = FindNearestEnemyInRange();

        if (targetEnemy != null)
        {

            Vector2 direction = (targetEnemy.transform.position - noteTransform.position).normalized;
            noteTransform.position = Vector2.MoveTowards(noteTransform.position, targetEnemy.transform.position, Time.deltaTime * trackSpeed);

            //lastDirection = direction;
            //// Perpendicular vector for wave motion
            //Vector2 perpendicular = new Vector2(-direction.y, direction.x);

            //// Apply sine wave offset
            //float wave = Mathf.Sin(Time.time * waveFrequency + waveOffset) * waveAmplitude;
            //Vector2 waveOffsetVector = perpendicular * wave;

            //// Move projectile
            //Vector2 nextPos = Vector2.MoveTowards(noteTransform.position, targetEnemy.transform.position, Time.deltaTime * trackSpeed);
            //noteTransform.position = nextPos + waveOffsetVector;

            //// Optional: Rotate to face target
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //noteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            //// Idle Sway (no target)
            //float idleWave = Mathf.Sin(Time.time * idleFrequency + idleOffset) * idleAmplitude;
            //Vector2 swayOffset = new Vector2(-idleDirection.y, idleDirection.x) * idleWave;

            //// Optionally drift slowly in idleDirection
            //Vector2 drift = idleDirection * (trackSpeed * 0.2f) * Time.deltaTime;

            //noteTransform.position += (Vector3)(swayOffset + drift);

            //// Optional: rotate to face movement direction
            //if (drift != Vector2.zero)
            //{
            //    noteTransform.right = idleDirection;
            //}

            despawner += Time.deltaTime;
            if (despawner > despawnTime) 
            { 
                poolManager.ReturnObjectToPool(this.gameObject);
                despawner = 0;
            };
        }



      



        //Vector3 dir = targetEnemy.transform.position - noteTransform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //noteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    GameObject FindNearestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("RadWorm");

        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector2.Distance(noteTransform.position, enemy.transform.position);
            if (enemyDistance < minDistance && enemyDistance <= trackingRange)
            {
                minDistance = enemyDistance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RadWorm"))
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            Debug.Log($"Hit enemy: {collision.gameObject.name}");

            //adding and Removing

            //poolManager.SpawnObject(collision.gameObject, collision.gameObject.transform.position, Quaternion.identity, poolManager.PoolType.GameObject);
            //poolManager.ReturnObjectToPool(collision.gameObject);
        }
    }
}