using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [Header("Datos de la Carrera")]
    public int totalLaps = 3;
    public int selectedCarIndex = 0;
    public string selectedTrackScene = "mundo2";

    [Header("Configuración Global")]
    public GameObject[] carPrefabs;
    public Sprite[] carSprites;

    // Lista de escenas de circuitos 
    public string[] trackScenes = { "mundo1", "mundo2" };
    public string[] trackDisplayNames = { "Easy", "Difficult" };

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Singleton 
        } else {
            Destroy(gameObject);
        }
    }
}