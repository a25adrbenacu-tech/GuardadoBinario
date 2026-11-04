using UnityEngine;

public class EnemySave : SaveableObject
{
    private ContadorESave contadorEnemigos;

    private void Awake()
    {
        //obtener contador enemigos
        contadorEnemigos = GetComponent<ContadorESave>();
    }

    public override void Guardar(SaveData datos)
    {
        if (datos is EnemySaveData enemyDat)
        {
            //guardar posicion, activo y contador enemigos
            enemyDat.enemyX = transform.position.x;
            enemyDat.enemyY = transform.position.y;
            enemyDat.enemyZ = transform.position.z;
            enemyDat.enemyActivo = gameObject.activeSelf;

            if (contadorEnemigos != null)
            {
                enemyDat.enemigosMuertos = contadorEnemigos.EnemigosMuertos;
            }
        }
    }

    public override void Cargar(SaveData datos)
    {
        if (datos is EnemySaveData enemyDat)
        {
            //cargar posicion, activo y contador enemigos
            transform.position = new Vector3(
                enemyDat.enemyX,
                enemyDat.enemyY,
                enemyDat.enemyZ
            );

            gameObject.SetActive(enemyDat.enemyActivo);

            if (contadorEnemigos != null)
            {
                contadorEnemigos.EnemigosMuertos = enemyDat.enemigosMuertos;
            }
        }
    }
}