using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour {
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;

    private void OnGUI () {
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    public void SetSelected() {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
