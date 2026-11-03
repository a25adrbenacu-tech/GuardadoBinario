
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;

    private PlayerSave jugador;
    public SpawnMonedas spawnMonedas;
    public int MonedasPlayer;
    void Awake()
    {
        jugador = GetComponent<PlayerSave>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = new Vector3(horizontal, 0, vertical);
        movimiento = movimiento.normalized;

        if (movimiento != Vector3.zero)
        {
            transform.Translate(
                movimiento * velocidad * Time.deltaTime,
                Space.World
            );

            if (jugador != null)
                jugador.NotificarCambio();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Moneda") == true) {
            
            Destroy(collision.gameObject);
            MonedasPlayer++;
            spawnMonedas.monedaDestruida();




        }
    }
}
