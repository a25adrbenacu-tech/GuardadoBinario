using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Text.RegularExpressions;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public float intervaloAutoguardado = 10f;

    private string rutaArchivo;

    //instancias de datos
    public PlayerSaveData playerData = new PlayerSaveData();
    public DoorSaveData doorData = new DoorSaveData();
    public EnemySaveData enemyData = new EnemySaveData();
    public CoinData coinData = new CoinData();

    private HashSet<SaveableObject> cambiosPendientes = new HashSet<SaveableObject>();

    private TMP_Text TextoMonedas => GameObject.Find("ContadorMonedas")?.GetComponent<TMP_Text>();
    private TMP_Text TextoEnemigos => GameObject.Find("enemigosDerrotados")?.GetComponent<TMP_Text>();

    private int LeerNumero(TMP_Text t)
    {
        if (t == null) return 0;
        Match m = Regex.Match(t.text, @"\d+");
        return m.Success ? int.Parse(m.Value) : 0;
    }

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
        //registrar estado inicial
        SaveableObject[] objetos = FindObjectsOfType<SaveableObject>(true);

        foreach (SaveableObject objeto in objetos)
        {
            PasarDatosAGuardar(objeto);
        }

        StartCoroutine(Autoguardado());
    }

    private void PasarDatosAGuardar(SaveableObject objeto)
    {
        //enrutar datos por tipo
        if (objeto is PlayerSave) objeto.Guardar(playerData);
        else if (objeto is DoorSave) objeto.Guardar(doorData);
        else if (objeto is EnemySave) objeto.Guardar(enemyData);
        else if (objeto is CoinSave) objeto.Guardar(coinData);
        else if (objeto is ContadorMSave) objeto.Guardar(coinData);
        else if (objeto is ContadorESave) objeto.Guardar(enemyData);    
    }

    public void RegistrarCambio(SaveableObject objeto)
    {
        if (objeto == null)
            return;

        PasarDatosAGuardar(objeto);
        cambiosPendientes.Add(objeto);
    }

    private IEnumerator Autoguardado()
    {
        //bucle autoguardado
        while (true)
        {
            yield return new WaitForSeconds(intervaloAutoguardado);
            GuardarPartida();
        }
    }

    public void GuardarPartida()
    {
        foreach (SaveableObject objeto in FindObjectsOfType<SaveableObject>(true))
            PasarDatosAGuardar(objeto);

        coinData.monedasPlayer = LeerNumero(TextoMonedas);
        enemyData.enemigosMuertos = LeerNumero(TextoEnemigos);

        Debug.Log($"GUARDANDO (leído del texto) -> monedas: {coinData.monedasPlayer}, enemigos: {enemyData.enemigosMuertos}");

        //escribir binario
        using (BinaryWriter writer = new BinaryWriter(File.Open(rutaArchivo, FileMode.Create)))
        {
            writer.Write(playerData.playerX);
            writer.Write(playerData.playerY);
            writer.Write(playerData.playerZ);
            writer.Write(playerData.playerVida);

            writer.Write(doorData.puertaAbierta);

            writer.Write(enemyData.enemyX);
            writer.Write(enemyData.enemyY);
            writer.Write(enemyData.enemyZ);
            writer.Write(enemyData.enemyActivo);
            writer.Write(enemyData.enemigosMuertos);

            writer.Write(coinData.monedasRestantes);
            writer.Write(coinData.monedasPlayer);
            writer.Write(coinData.totalMonedasEscena);
            writer.Write(coinData.posicionesMonedas.Count);
            foreach (PosicionMoneda p in coinData.posicionesMonedas)
            {
                writer.Write(p.x);
                writer.Write(p.y);
                writer.Write(p.z);
            }
        }


        cambiosPendientes.Clear();
    }

    public void CargarPartida()
    {
        if (!File.Exists(rutaArchivo))
        {
            return;
        }

        //leer binario
        using (BinaryReader reader = new BinaryReader(File.Open(rutaArchivo, FileMode.Open)))
        {
            playerData.playerX = reader.ReadSingle();
            playerData.playerY = reader.ReadSingle();
            playerData.playerZ = reader.ReadSingle();
            playerData.playerVida = reader.ReadInt32();

            doorData.puertaAbierta = reader.ReadBoolean();

            enemyData.enemyX = reader.ReadSingle();
            enemyData.enemyY = reader.ReadSingle();
            enemyData.enemyZ = reader.ReadSingle();
            enemyData.enemyActivo = reader.ReadBoolean();
            enemyData.enemigosMuertos = reader.ReadInt32();

            coinData.monedasRestantes = reader.ReadInt32();
            coinData.monedasPlayer = reader.ReadInt32();
            coinData.totalMonedasEscena = reader.ReadInt32();
            
            int cantidad = reader.ReadInt32();
            coinData.posicionesMonedas.Clear();
            for (int i = 0; i < cantidad; i++)
            {
                coinData.posicionesMonedas.Add(new PosicionMoneda
                {
                    x = reader.ReadSingle(),
                    y = reader.ReadSingle(),
                    z = reader.ReadSingle()
                });
            }
        }

        //aplicar datos a objetos
        SaveableObject[] objetos = FindObjectsOfType<SaveableObject>(true);

        foreach (SaveableObject objeto in objetos)
        {
            if (objeto is PlayerSave) objeto.Cargar(playerData);
            else if (objeto is DoorSave) objeto.Cargar(doorData);
            else if (objeto is EnemySave) objeto.Cargar(enemyData);
            else if (objeto is CoinSave) objeto.Cargar(coinData);
            else if (objeto is ContadorMSave) objeto.Cargar(coinData);      
            else if (objeto is ContadorESave) objeto.Cargar(enemyData);
        }

        foreach (ContadorMSave c in FindObjectsOfType<ContadorMSave>(true))
            c.MonedasPlayer = coinData.monedasPlayer;

        foreach (ContadorESave c in FindObjectsOfType<ContadorESave>(true))
            c.EnemigosMuertos = enemyData.enemigosMuertos;

        if (TextoMonedas != null) TextoMonedas.text = "Monedas: " + coinData.monedasPlayer;
        if (TextoEnemigos != null) TextoEnemigos.text = "Enemigos derrotados: " + enemyData.enemigosMuertos;

        Debug.Log($"CARGADO -> monedas: {coinData.monedasPlayer}, enemigos: {enemyData.enemigosMuertos}");

        cambiosPendientes.Clear();
    }
}