using System;
using Assets.Scripts.Voice;
using TMPro; // Necesario para los textos
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

  [Header("UI Referencias")]
  [SerializeField] private TMP_Text _carNameText;
  [SerializeField] private TMP_Text _trackNameText;
  [SerializeField] private TMP_Text _lapsText;
  [SerializeField] private Image _carPreviewImage;

  // Variables locales para navegar por el menú
  private int _carIndex = 0;
  private int _trackIndex = 0;
  private int _laps = 3;

  private void Start() {
    // UpdateUI(); // Primero se muestra que es cada cosa
    VoiceManager[] voiceMgrs = FindObjectsByType<VoiceManager>(FindObjectsSortMode.None);
    if (voiceMgrs != null && voiceMgrs.Length > 0) {
      voiceMgrs[0].RegisterReceiver(gameObject);
    }
  }

  // Funciones para los botones
  /// <summary>
  /// Avanza al siguiente coche, volviendo al primero si se supera el límite.
  /// </summary>
  public void NextCar() {
    _carIndex++;
    if (_carIndex >= GameManager.Instance.CarPrefabs.Length) {
      _carIndex = 0;
    }

    UpdateUI();
  }

  /// <summary>
  /// Retrocede al coche anterior, volviendo al último si se supera el límite.
  /// </summary>
  public void PrevCar() {
    _carIndex--;
    if (_carIndex < 0) {
      _carIndex = GameManager.Instance.CarPrefabs.Length - 1;
    }

    UpdateUI();
  }


  /// <summary>
  /// Avanza al siguiente circuito, volviendo al primero si se supera el límite.
  /// </summary>
  public void NextTrack() {
    _trackIndex++;
    if (_trackIndex >= GameManager.Instance.TrackScenes.Length) {
      _trackIndex = 0;
    }

    Console.WriteLine("Next Track: " + _trackIndex);
    UpdateUI();
  }

  /// <summary>
  /// Retrocede al circuito anterior, volviendo al último si se supera el límite.
  /// </summary>
  public void PrevTrack() {
    _trackIndex--;
    if (_trackIndex < 0) {
      _trackIndex = GameManager.Instance.TrackScenes.Length - 1;
    }

    UpdateUI();
  }


  /// <summary>
  /// Aumenta el número de vueltas, con un máximo de 99.
  /// </summary>
  public void IncreaseLaps() {
    _laps++;
    if (_laps > 99) {
      _laps = 99; // Límite lógico
    }

    UpdateUI();
  }

  /// <summary>
  /// Disminuye el número de vueltas, con un mínimo de 1.
  /// </summary>
  public void DecreaseLaps() {
    _laps--;
    if (_laps < 1) {
      _laps = 1;
    }

    UpdateUI();
  }

  /// <summary>
  /// Inicia la carrera con las opciones seleccionadas.
  /// </summary>
  public void StartRace() {
    // Guardamos los datos seleccionados en el GameManager
    GameManager.Instance.SelectedCarIndex = _carIndex;
    GameManager.Instance.SelectedTrackScene = GameManager.Instance.TrackScenes[_trackIndex];
    GameManager.Instance.TotalLaps = _laps;
    SceneManager.LoadScene(GameManager.Instance.SelectedTrackScene);
  }

  /// <summary>
  /// Actualiza la UI del menú principal con los datos actuales.
  /// </summary>
  private void UpdateUI() {
    GameObject carPrefab = GameManager.Instance.CarPrefabs[_carIndex];
    _carNameText.text = carPrefab.name;
    if (GameManager.Instance.CarSprites.Length > _carIndex) {
      _carPreviewImage.sprite = GameManager.Instance.CarSprites[_carIndex];
      _carPreviewImage.preserveAspect = true;
    }

    _trackNameText.text = GameManager.Instance.TrackDisplayNames[_trackIndex];
    _lapsText.text = _laps.ToString();
  }
}