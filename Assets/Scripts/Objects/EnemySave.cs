using UnityEngine;

public class EnemySave : SaveableObject
{
    public override void Guardar(SaveData datos)
    {
        datos.enemyX = transform.position.x;
        datos.enemyY = transform.position.y;
        datos.enemyZ = transform.position.z;
        datos.enemyActivo = gameObject.activeSelf;
    }

    public override void Cargar(SaveData datos)
    {
        transform.position = new Vector3(
            datos.enemyX,
            datos.enemyY,
            datos.enemyZ
        );

        gameObject.SetActive(datos.enemyActivo);

        Debug.Log("Enemigo cargado. Activo: " + datos.enemyActivo);
    }
}
