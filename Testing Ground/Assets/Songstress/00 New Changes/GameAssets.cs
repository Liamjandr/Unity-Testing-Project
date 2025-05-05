using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private static GameAssets _assets;
    public static GameAssets asset
    {
        get
        {
            if (_assets == null) _assets = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _assets;
        }
    }

    //Attack Prefabs
    public GameObject SampleNote;
    public GameObject SampleEighth;
    public GameObject SampleQuarter;
    public GameObject SampleHalf;
    public GameObject Charged_1;

    //Enemy Prefabs
    public GameObject RadWorm;






}
