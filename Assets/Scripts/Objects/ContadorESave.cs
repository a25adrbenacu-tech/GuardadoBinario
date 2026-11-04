using UnityEngine;
using TMPro;

public class ContadorESave : SaveableObject
{
    [SerializeField] private int enemigosMuertos;
    public TMP_Text enemigos;

    public int EnemigosMuertos
    {
        get => enemigosMuertos;
        set { enemigosMuertos = value; ActualizarTexto(); }
    }

    private void Update()
    {
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (enemigos != null)
            enemigos.text = "Enemigos derrotados: " + enemigosMuertos;
    }

    public void IncrementarEnemigosMuertos()
    {
        enemigosMuertos++;
        ActualizarTexto();
        NotificarCambio();
    }


    public override void Guardar(SaveData datos)
    {
        if (datos is EnemySaveData enemyDat)
            enemyDat.enemigosMuertos = enemigosMuertos;
    }

    public override void Cargar(SaveData datos)
    {
        if (datos is EnemySaveData enemyDat)
        {
            enemigosMuertos = enemyDat.enemigosMuertos;
            ActualizarTexto();
        }
    }
}