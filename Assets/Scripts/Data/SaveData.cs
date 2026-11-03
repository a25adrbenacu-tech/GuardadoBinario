using System;

[Serializable]
public class SaveData
{
    // Datos del jugador
    public float playerX;
    public float playerY;
    public float playerZ;
    public int playerVida;

    // Datos de la puerta
    public bool puertaAbierta;

    // Datos del enemigo
    public float enemyX;
    public float enemyY;
    public float enemyZ;
    public bool enemyActivo;
}
