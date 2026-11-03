using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public PlayerMovement playermovement;
    public TMP_Text monedas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        monedas.text = "Monedas: " + playermovement.MonedasPlayer;
        
    }
}
