using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager main;

    public Transform StartPoint;
    public Transform[] path;

    public void Awake()
    {
        main = this;
    }

}
