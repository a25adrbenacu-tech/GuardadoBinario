
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public DoorSave puerta;

    public void CambiarPuerta()
    {
        if (puerta.puertaAbierta)
        {
            puerta.CerrarPuerta();
        }
        else
        {
            puerta.AbrirPuerta();
        }
    }
}
