using UnityEngine;

public class Plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor; //

    private GameObject bird;
    private Color startColor; //

    private void Start()
    {
        startColor = sr.color; //
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor; //
    }

    private void OnMouseExit()
    {
        sr.color = startColor; //
    }

    private void OnMouseDown()
    {
        Debug.Log("Build bird here: " + name);

        if (bird != null) return;


        Bird birdToBuild = BuildManager.main.GetSelectedBird();

        if (birdToBuild.cost > LevelManager.main.currency) {
            Debug.Log("You cant afford this bird");
            return;
        }

        LevelManager.main.SpendCurrency(birdToBuild.cost);

        bird = Instantiate(birdToBuild.prefab, transform.position, Quaternion.identity);

    }

}
