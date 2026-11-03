using UnityEngine;

public class SaveUI : MonoBehaviour
{
    public void Guardar()
    {
        SaveManager.Instance.GuardarPartida();
    }

    public void Cargar()
    {
        SaveManager.Instance.CargarPartida();
    }
}
