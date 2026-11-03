
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 5f;

    private PlayerSave jugador;

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
}
