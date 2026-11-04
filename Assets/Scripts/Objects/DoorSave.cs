using UnityEngine;

public class DoorSave : SaveableObject
{
    [Header("Estado de la puerta")]
    public bool puertaAbierta = false;

    public override void Guardar(SaveData datos)
    {
        if (datos is DoorSaveData doorDat)
        {
            //guardar estado puerta
            doorDat.puertaAbierta = puertaAbierta;
        }
    }

    public override void Cargar(SaveData datos)
    {
        if (datos is DoorSaveData doorDat)
        {
            //cargar estado puerta
            puertaAbierta = doorDat.puertaAbierta;

            if (puertaAbierta)
                transform.rotation = Quaternion.Euler(0, 90, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void AbrirPuerta()
    {
        puertaAbierta = true;
        transform.rotation = Quaternion.Euler(0, 90, 0);

        NotificarCambio();
    }

    public void CerrarPuerta()
    {
        puertaAbierta = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        NotificarCambio();
    }
}