using UnityEngine;

public class MonedasRecoger : MonoBehaviour
{
    private bool recogida = false;

    private void OnTriggerEnter(Collider other)
    {
        if (recogida || !other.CompareTag("Player")) return;
        recogida = true;

        ContadorMSave contador = FindObjectOfType<ContadorMSave>(true);
        if (contador != null)
            contador.IncrementarMonedasPlayer();

        Destroy(gameObject);
    }
}