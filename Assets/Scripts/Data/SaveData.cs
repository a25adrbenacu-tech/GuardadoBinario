using System;

[Serializable]
public abstract class SaveData
{
}

[Serializable]
public class PlayerSaveData : SaveData
//datos jugador
{
    public float playerX;
    public float playerY;
    public float playerZ;
    public int playerVida;
}

[Serializable]
public class DoorSaveData : SaveData
//datos puerta
{
    public bool puertaAbierta;
}

[Serializable]
public class EnemySaveData : SaveData
//datos enemigo
{
    public float enemyX;
    public float enemyY;
    public float enemyZ;
    public bool enemyActivo;
    public int enemigosMuertos;
}

[Serializable]
public class CoinSaveData : SaveData
//datos monedas
{
    public int monedasRestantes;
    public int monedasPlayer;
}