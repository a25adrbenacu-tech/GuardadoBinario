using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [Header("Autoguardado")]
    public float intervaloAutoguardado = 10f;

    private string rutaArchivo;

    // Datos que se guardarán temporalmente
    private SaveData datosTemporales = new SaveData();

    // Objetos que han cambiado
    private HashSet<SaveableObject> cambiosPendientes =
        new HashSet<SaveableObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        rutaArchivo = Application.persistentDataPath + "/partida.dat";
    }

    private void Start()
    {
        // Registrar el estado inicial de todos los objetos
        SaveableObject[] objetos =
            FindObjectsOfType<SaveableObject>(true);

        foreach (SaveableObject objeto in objetos)
        {
            objeto.Guardar(datosTemporales);
        }

        StartCoroutine(Autoguardado());
    }

    // Registrar los cambios de un objeto
    public void RegistrarCambio(SaveableObject objeto)
    {
        if (objeto == null)
            return;

        objeto.Guardar(datosTemporales);

        cambiosPendientes.Add(objeto);

        Debug.Log("Cambio registrado: " + objeto.name);
    }

    // Autoguardado automático
    private IEnumerator Autoguardado()
    {
        Debug.Log("Autoguardado iniciado.");

        while (true)
        {
            yield return new WaitForSeconds(intervaloAutoguardado);

            Debug.Log("Realizando autoguardado...");

            GuardarPartida();

            Debug.Log("Autoguardado completado.");
        }
    }

    // Guardar los datos temporales en el archivo definitivo
    public void GuardarPartida()
    {
        using (BinaryWriter writer = new BinaryWriter(
            File.Open(rutaArchivo, FileMode.Create)))
        {
            writer.Write(datosTemporales.playerX);
            writer.Write(datosTemporales.playerY);
            writer.Write(datosTemporales.playerZ);
            writer.Write(datosTemporales.playerVida);

            writer.Write(datosTemporales.puertaAbierta);

            writer.Write(datosTemporales.enemyX);
            writer.Write(datosTemporales.enemyY);
            writer.Write(datosTemporales.enemyZ);
            writer.Write(datosTemporales.enemyActivo);
        }

        cambiosPendientes.Clear();

        Debug.Log("Partida guardada correctamente.");
    }

    // Cargar la partida
    public void CargarPartida()
    {
        if (!File.Exists(rutaArchivo))
        {
            Debug.Log("No existe ninguna partida guardada.");
            return;
        }

        using (BinaryReader reader = new BinaryReader(
            File.Open(rutaArchivo, FileMode.Open)))
        {
            datosTemporales.playerX = reader.ReadSingle();
            datosTemporales.playerY = reader.ReadSingle();
            datosTemporales.playerZ = reader.ReadSingle();
            datosTemporales.playerVida = reader.ReadInt32();

            datosTemporales.puertaAbierta = reader.ReadBoolean();

            datosTemporales.enemyX = reader.ReadSingle();
            datosTemporales.enemyY = reader.ReadSingle();
            datosTemporales.enemyZ = reader.ReadSingle();
            datosTemporales.enemyActivo = reader.ReadBoolean();
        }

        SaveableObject[] objetos =
            FindObjectsOfType<SaveableObject>(true);

        foreach (SaveableObject objeto in objetos)
        {
            objeto.Cargar(datosTemporales);
        }

        cambiosPendientes.Clear();

        Debug.Log("Partida cargada correctamente.");
    }
}
