using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySave enemigo;
    private float cdenemigo = 1;
    private bool enemy = true;
    public int enemigosMuertos = 0;

    private void Awake()
    {
        enemigo = GetComponent<EnemySave>();
    }

    void Update()
    {
        cdRespawn();
    }

    public void EliminarEnemigo()
    {
        if (enemigo != null)
            enemigo.NotificarCambio();

        gameObject.SetActive(false);

        Debug.Log("El enemigo ha sido eliminado.");
    }

    public void ActivarEnemigo()
    {
        gameObject.SetActive(true);

        if (enemigo != null)
            enemigo.NotificarCambio();

        Debug.Log("El enemigo ha aparecido.");
    }

    public void cdRespawn()
    {
        if (enemy == false)
        {
            cdenemigo -= Time.deltaTime;
            if (cdenemigo <= 0)
            {
                gameObject.SetActive(true);
                cdenemigo = 1;
                enemy = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ContadorESave contador = FindObjectOfType<ContadorESave>(true);
            if (contador != null)
                contador.IncrementarEnemigosMuertos();

            gameObject.SetActive(false);
            enemy = false;
        }
    }
}
