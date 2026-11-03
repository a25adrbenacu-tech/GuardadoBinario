using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public PlayerMovement playermovement;
    public EnemyController EnemyCotroller;
    public TMP_Text monedas;
    public TMP_Text enemigos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        monedas.text = "Monedas: " + playermovement.MonedasPlayer;
        enemigos.text = "Enemigos derrotados: " + EnemyCotroller.enemigosMuertos;
        
    }
}
