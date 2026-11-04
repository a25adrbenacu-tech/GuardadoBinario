using UnityEngine;
using TMPro;

public class ContadorMSave : SaveableObject
{
    [SerializeField] private int monedasRestantes;
    [SerializeField] private int monedasPlayer;
    public TMP_Text monedas;

    public int MonedasRestantes { get => monedasRestantes; set => monedasRestantes = value; }
    public int MonedasPlayer
    {
        get => monedasPlayer;
        set { monedasPlayer = value; ActualizarTexto(); }
    }

    private void Update()
    {
        //monedas que quedan en escena (tag CM)
        monedasRestantes = GameObject.FindGameObjectsWithTag("CM").Length;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (monedas != null)
            monedas.text = "Monedas: " + monedasPlayer;
    }

    public void IncrementarMonedasPlayer()
    {
        monedasPlayer++;
        ActualizarTexto();
        NotificarCambio();
    }

    public void RestarMonedaRestante()
    {
        monedasRestantes--;
    }

    public override void Guardar(SaveData datos)
    {
        if (datos is CoinData coinData)
            coinData.monedasPlayer = monedasPlayer;
    }

    public override void Cargar(SaveData datos)
    {
        if (datos is CoinData coinData)
        {
            monedasPlayer = coinData.monedasPlayer;
            ActualizarTexto();
        }
    }
}