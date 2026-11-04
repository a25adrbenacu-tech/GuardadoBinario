using UnityEngine;

public class CoinSave : SaveableObject
{
    public GameObject monedaPrefab;   

    private const string TAG_MONEDA = "Moneda";   

    public override void Guardar(SaveData datos)
    {
        if (datos is CoinData coinData)
        {
            GameObject[] monedas = GameObject.FindGameObjectsWithTag(TAG_MONEDA);
            coinData.totalMonedasEscena = monedas.Length;
            coinData.monedasRestantes = monedas.Length;

            coinData.posicionesMonedas.Clear();
            foreach (GameObject m in monedas)
            {
                Vector3 p = m.transform.position;
                coinData.posicionesMonedas.Add(new PosicionMoneda { x = p.x, y = p.y, z = p.z });
            }
        }
    }

    public override void Cargar(SaveData datos)
    {
        if (datos is CoinData coinData)
        {
            if (monedaPrefab == null)
            {
                Debug.LogError("CoinSave: falta asignar monedaPrefab en el Inspector.");
                return;
            }

            foreach (GameObject m in GameObject.FindGameObjectsWithTag(TAG_MONEDA))
                Destroy(m);

            foreach (PosicionMoneda p in coinData.posicionesMonedas)
                Instantiate(monedaPrefab, new Vector3(p.x, p.y, p.z), Quaternion.identity);

            SpawnMonedas spawner = FindObjectOfType<SpawnMonedas>();
            if (spawner != null)
                spawner.ReasignarMonedas();
        }
    }

}