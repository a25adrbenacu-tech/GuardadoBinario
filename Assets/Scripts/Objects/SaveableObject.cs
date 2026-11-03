using UnityEngine;

public abstract class SaveableObject : MonoBehaviour
{
    public abstract void Guardar(SaveData datos);

    public abstract void Cargar(SaveData datos);

    // Avisar al sistema cuando cambia el objeto
    public void NotificarCambio()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.RegistrarCambio(this);
        }
    }
}
