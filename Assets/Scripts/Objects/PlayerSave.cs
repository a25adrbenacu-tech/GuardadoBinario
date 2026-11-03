using UnityEngine;

public class PlayerSave : SaveableObject
{
    [Header("Datos del jugador")]
    public int vidaActual = 100;

    public override void Guardar(SaveData datos)
    {
        // Guardar posición
        datos.playerX = transform.position.x;
        datos.playerY = transform.position.y;
        datos.playerZ = transform.position.z;

        // Guardar vida
        datos.playerVida = vidaActual;
    }

    public override void Cargar(SaveData datos)
    {
        // Recuperar posición
        transform.position = new Vector3(
            datos.playerX,
            datos.playerY,
            datos.playerZ
        );

        // Recuperar vida
        vidaActual = datos.playerVida;
    }
}
