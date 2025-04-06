using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager main;

    public Transform StartPoint;
    public Transform[] path;

    public int currency;

    public void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            // COMPRAR PÁJARO
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("No tiene suficiente dinero");
            return false;
        }
    }

}
