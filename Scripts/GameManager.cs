using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour {
  public static GameManager Instance { get; private set; }

  public GameObject VictoryPanel;
  public VRGazeClicker GazeScript;

  public void OnPlayerWin() {
    // 1. Mostramos el panel de botones
    VictoryPanel.SetActive(true);
    // 2. ACTIVAMOS el script de la mirada
    if (GazeScript != null) {
      GazeScript.enabled = true;
    }
  }

  [Header("Datos de la Carrera")]
  [FormerlySerializedAs("totalLaps")]
  [SerializeField] private int _totalLaps = 3;
  [FormerlySerializedAs("SelectedCarIndex")]
  [SerializeField] private int _selectedCarIndex;
  [FormerlySerializedAs("selectedTrackScene")]
  [SerializeField] private string _selectedTrackScene = "mundo2";

  public int TotalLaps {
    get => _totalLaps;
    set => _totalLaps = value;
  }

  public int SelectedCarIndex {
    get => _selectedCarIndex;
    set => _selectedCarIndex = value;
  }

  public GameObject SelectedCarPrefab {
    get {
      if (SelectedCarIndex >= 0 && SelectedCarIndex < CarPrefabs.Length) {
        return CarPrefabs[SelectedCarIndex];
      }
      return null;
    }
  }

  public string SelectedTrackScene {
    get => _selectedTrackScene;
    set => _selectedTrackScene = value;
  }

  [Header("Configuración Global")]
  [FormerlySerializedAs("carPrefabs")]
  [SerializeField] private GameObject[] _carPrefabs;
  [FormerlySerializedAs("carSprites")]
  [SerializeField] private Sprite[] _carSprites;
  [FormerlySerializedAs("trackScenes")]
  [SerializeField] private string[] _trackScenes = { "mundo1", "mundo2" };
  [FormerlySerializedAs("trackDisplayNames")]
  [SerializeField] private string[] _trackDisplayNames = { "Easy", "Difficult" };

  public GameObject[] CarPrefabs => _carPrefabs;
  public Sprite[] CarSprites => _carSprites;
  public string[] TrackScenes => _trackScenes;
  public string[] TrackDisplayNames => _trackDisplayNames;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject); // Singleton 
    } else {
      Destroy(gameObject);
    }
  }


}