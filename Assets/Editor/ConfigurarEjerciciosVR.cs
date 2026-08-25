using UnityEngine;
using UnityEditor;

public class ConfigurarEjerciciosVR : EditorWindow
{
    [MenuItem("Herramientas VR/Configurar Zonas y Señalética")]
    public static void ConfigurarTodo()
    {
        // 1. Configurar Señal Paila
        var senalPaila = GameObject.Find("SEÑAL_PAILA");
        var pailaInteractuable = GameObject.Find("pailainteractuable");
        ConfigurarDetector(senalPaila, pailaInteractuable, "Assets/SONIDOS/freesound_community-tada-fanfare-a-6313.mp3");

        // 2. Configurar Señal Planta
        var senalPlanta = GameObject.Find("señal_planta");
        var plantaInteractuable = GameObject.Find("plantainteractuable");
        ConfigurarDetector(senalPlanta, plantaInteractuable, "Assets/SONIDOS/freesound_community-tada-fanfare-a-6313.mp3");

        // 3. Crear o Configurar Señal de Inicio
        var senalInicio = GameObject.Find("Senal_Inicio");
        if (senalInicio == null)
        {
            senalInicio = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            senalInicio.name = "Senal_Inicio";
            senalInicio.transform.position = new Vector3(0, 0.1f, 1.5f); // Posición inicial de ejemplo
            senalInicio.transform.localScale = new Vector3(1f, 0.05f, 1f);
            
            var renderer = senalInicio.GetComponent<Renderer>();
            renderer.sharedMaterial = new Material(Shader.Find("Standard")) { color = Color.yellow };
        }

        var col = senalInicio.GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
        
        var detectorJugador = senalInicio.GetComponent<DetectorJugadorVR>();
        if (detectorJugador == null) detectorJugador = senalInicio.AddComponent<DetectorJugadorVR>();
        
        detectorJugador.tagJugador = "Player";
        
        // Encontrar un objeto de instrucciones (ej. Tutorial Player o Canvas)
        var instrucciones = GameObject.Find("Tutorial Player") ?? GameObject.Find("Canvas");
        if (instrucciones != null)
        {
            detectorJugador.instrucciones = instrucciones;
            instrucciones.SetActive(false); // Apagar para que se enciendan cuando el jugador llegue
        }

        Debug.Log("¡Configuración de zonas VR completada con éxito!");
    }

    private static void ConfigurarDetector(GameObject senal, GameObject interactuable, string audioPath)
    {
        if (senal == null || interactuable == null) return;

        var collider = senal.GetComponent<Collider>();
        if (collider != null) collider.isTrigger = true;
        
        var detector = senal.GetComponent<DetectorZonaVR>();
        if (detector == null) detector = senal.AddComponent<DetectorZonaVR>();
        detector.objetoEsperado = interactuable;
        
        var audioSource = senal.GetComponent<AudioSource>();
        if (audioSource == null) audioSource = senal.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        
        var audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioPath);
        if (audioClip != null) audioSource.clip = audioClip;
        
        // Limpiamos los eventos anteriores para no duplicar
        UnityEditor.Events.UnityEventTools.RemovePersistentListener(detector.alCompletarExito, audioSource.Play);
        // Agregamos la acción de reproducir el audio
        UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(detector.alCompletarExito, audioSource.Play);
    }
}
