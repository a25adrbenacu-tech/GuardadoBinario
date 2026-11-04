using UnityEngine;

public class SpawnMonedas : MonoBehaviour
{
    public Transform[] ubiMonedas;
    public GameObject moneda;
    public float cdmonedas = 2f;  

    private GameObject[] monedasActuales;
    private float[] temporizadores;

    void Start()
    {
        monedasActuales = new GameObject[ubiMonedas.Length];
        temporizadores = new float[ubiMonedas.Length];

       
        for (int i = 0; i < ubiMonedas.Length; i++)
            CrearMoneda(i);
    }

    void Update()
    {
        for (int i = 0; i < ubiMonedas.Length; i++)
        {
            
            if (monedasActuales[i] == null)
            {
                temporizadores[i] -= Time.deltaTime;
                if (temporizadores[i] <= 0)
                    CrearMoneda(i);
            }
        }
    }

    private void CrearMoneda(int i)
    {
        monedasActuales[i] = Instantiate(moneda, ubiMonedas[i].position, ubiMonedas[i].rotation);
        temporizadores[i] = cdmonedas;
    }

    
    public void monedaDestruida()
    {
        Debug.Log("deu");
    }

    // tras cargar partida: asigna a cada posición la moneda que haya en ella
    public void ReasignarMonedas()
    {
        GameObject[] enEscena = GameObject.FindGameObjectsWithTag("Moneda");

        for (int i = 0; i < ubiMonedas.Length; i++)
        {
            monedasActuales[i] = null;
            temporizadores[i] = cdmonedas;

            foreach (GameObject m in enEscena)
            {
                if (m != null && Vector3.Distance(m.transform.position, ubiMonedas[i].position) < 0.5f)
                {
                    monedasActuales[i] = m;
                    break;
                }
            }
        }
    }
}