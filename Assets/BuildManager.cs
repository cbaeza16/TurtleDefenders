using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] birdPrefabs;
    [SerializeField] private Bird[] birds;

    private int selectedBird = 0;

    private void Awake()
    {
        main = this;
    }

    public Bird GetSelectedBird()
    {
        return birds[selectedBird];
    }
    
    public void SetSelectedBird(int _selectedBird) {
        //Debug.Log("CAMBIOOOOOO");
        selectedBird = _selectedBird;
    }
}
