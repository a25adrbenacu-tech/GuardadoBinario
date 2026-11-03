using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemySave enemigo;

    private void Awake()
    {
        enemigo = GetComponent<EnemySave>();
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
}
