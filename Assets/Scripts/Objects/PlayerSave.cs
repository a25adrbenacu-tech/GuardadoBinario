using UnityEngine;

public class PlayerSave : SaveableObject
{
    [Header("Datos del jugador")]
    public int vidaActual = 100;

    public override void Guardar(SaveData datos)
    {
        if (datos is PlayerSaveData playerDat)
        {
            //guardar posicion y vida
            playerDat.playerX = transform.position.x;
            playerDat.playerY = transform.position.y;
            playerDat.playerZ = transform.position.z;
            playerDat.playerVida = vidaActual;
        }
    }

    public override void Cargar(SaveData datos)
    {
        if (datos is PlayerSaveData playerDat)
        {
            //cargar posicion y vida
            transform.position = new Vector3(
                playerDat.playerX,
                playerDat.playerY,
                playerDat.playerZ
            );

            vidaActual = playerDat.playerVida;
        }
    }
}