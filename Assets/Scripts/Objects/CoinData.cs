using System;
using System.Collections.Generic;

[Serializable]
public struct PosicionMoneda
{
    public float x, y, z;
}

[Serializable]
public class CoinData : SaveData
//datos monedas
{
    public int monedasRestantes;
    public int monedasPlayer;
    public int totalMonedasEscena;
    public List<PosicionMoneda> posicionesMonedas = new List<PosicionMoneda>();
}