using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager main;

    [Header("References")]
    [SerializeField] private GameObject[] birdPrefabs;

    private int selectedBird = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedBird()
    {
        return birdPrefabs[selectedBird];
    }
    
}
