using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private SampleInstant sampleInst;

    private void Awake()
    {
        sampleInst = GetComponent<SampleInstant>();
    }
  

}
