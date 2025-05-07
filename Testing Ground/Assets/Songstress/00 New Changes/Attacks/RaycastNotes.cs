using UnityEngine;
using System.Collections;
using System.Linq;


public class RaycastNotes : MonoBehaviour
{
    private float trackSpeedX = 10f;

    private GameObject enemy;
    private Transform noteTransform;
    private int attackHits;
    //private bool inRange;
    //private Collider2D col2D;
    void Start()
    {
        GameObject[] enemies = NearestEnemy();
        enemy = enemies[0];
        noteTransform = GetComponent<Transform>();
        //col2D = GetComponent<Collider2D>();
    }


    void Update()
    {
        GameObject[] enemies = NearestEnemy();
        RaycastHit2D[] rayDistance = new RaycastHit2D[enemies.Length];
        Debug.Log("Enemy Count: "+ enemies.Length);

        RaycastHit2D rayClosest = rayDistance[0];

        for (int i = 0; i < enemies.Length; i++)
        {
            rayDistance[i] = Physics2D.Raycast(noteTransform.position, enemies[i].transform.position);

            rayClosest = rayDistance[0];
            //Debug.Log($"RayClosest = {rayClosest.distance}");
            //Debug.Log($"rayDistance[1] = {rayDistance[1].distance}");

            if (rayClosest.distance < rayDistance[i].distance)
            {
                rayClosest = rayDistance[i];
                enemy = enemies[i];
                Debug.Log($"closest enemy: " + enemy.transform);
            }

            Debug.DrawRay(noteTransform.position, enemies[i].transform.position - noteTransform.position, Color.green);
            Debug.Log("Hello");

            Debug.Log(enemies[i].transform + " | " + rayDistance[i].distance);
        }

        Debug.Log($"Collided with " + enemy.transform);

        noteTransform.position = Vector2.MoveTowards(noteTransform.position, enemy.transform.position, Time.deltaTime * trackSpeedX);
    
    }

    private void FixedUpdate()
    {
        //RaycastHit2D ray = Physics2D.Raycast(noteTransform.position, enemy.transform.position - noteTransform.position);
        
    }
    GameObject[] NearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("RadWorm");
        return enemies;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RadWorm")
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            ++attackHits;
            Debug.Log($"Collided with {collision.gameObject}");
        }
    }
}
