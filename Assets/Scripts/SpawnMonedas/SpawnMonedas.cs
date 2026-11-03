using UnityEditor;
using UnityEngine;

public class SpawnMonedas : MonoBehaviour
{
    public Transform[] ubiMonedas;
    public GameObject moneda;
    public int monedasRestantes = 0;
    public float cdmonedas = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpameoMonedas();
    }

    // Update is called once per frame
    void Update()
    {
        SpameoMonedas();
    }

    public void SpameoMonedas()
    {
        
        if (monedasRestantes == 0)
        {
            cdmonedas -= Time.deltaTime;
            if (cdmonedas <= 0)
            {
                Instantiate(moneda, ubiMonedas[0].transform.position, ubiMonedas[0].transform.rotation);
                Instantiate(moneda, ubiMonedas[1].transform.position, ubiMonedas[1].transform.rotation);
                monedasRestantes = 2;
                cdmonedas = 2;
            }
        }

    }

    public void monedaDestruida()
    {
        monedasRestantes--;
        Debug.Log("deu");
    }

    


}
