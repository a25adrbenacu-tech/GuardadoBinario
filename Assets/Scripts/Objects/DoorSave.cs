using UnityEngine;

public class DoorSave : SaveableObject
{
    [Header("Estado de la puerta")]
    public bool puertaAbierta = false;

    public override void Guardar(SaveData datos)
    {
        datos.puertaAbierta = puertaAbierta;
    }

    public override void Cargar(SaveData datos)
    {
        puertaAbierta = datos.puertaAbierta;

        if (puertaAbierta)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);

        Debug.Log("Estado de la puerta cargado: " +
            (puertaAbierta ? "ABIERTA" : "CERRADA"));
    }

    public void AbrirPuerta()
    {
        puertaAbierta = true;
        transform.rotation = Quaternion.Euler(0, 90, 0);

        NotificarCambio();

        Debug.Log("La puerta se ha ABIERTO.");
    }

    public void CerrarPuerta()
    {
        puertaAbierta = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        NotificarCambio();

        Debug.Log("La puerta se ha CERRADO.");
    }
}
