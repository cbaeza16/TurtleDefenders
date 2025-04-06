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

        GameObject birdToBuild = BuildManager.main.GetSelectedBird();
        bird = Instantiate(birdToBuild, transform.position, Quaternion.identity);

    }

}
