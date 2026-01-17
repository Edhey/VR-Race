using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class HUDManager : MonoBehaviour {
  [Header("UI Elements (Telemetry)")]
  [SerializeField] private RectTransform _needleTransform;

  [Header("UI Elements (Race Info)")]
  [SerializeField] private TMP_Text _currentCheckpointText;
  [SerializeField] private TMP_Text _lapText;
  [SerializeField] private RawImage _skippedPointImage;
  [SerializeField] private RawImage _raceFinishedImage;
  [SerializeField] private TMP_Text _timeText;

  [Header("Skipped Point Warning Settings")]
  [SerializeField] private float _skippedPointTextWarningDuration = 3f;
  [SerializeField, Range(0.1f, 2f)] private float _fadeDuration = 1f;

  [Header("Settings")]
  [SerializeField] private float _minNeedleAngle = 20f;
  [SerializeField] private float _maxNeedleAngle = -200f;
  [SerializeField] private float _maxSpeed = 200f;
  [SerializeField] private float _maxRPM = 8000f;

  private CheckpointHandler _checkpointHandler;

  private float _elapsedTime = 0f;
  private bool _timerIsRunning = false;
  private string _baseLapTemplate;
  private string _baseCheckpointTemplate;
  private string _baseTimeTemplate;
  private Coroutine _warningCoroutine;
  private Canvas _finalScreenReticleCanvas;
  private VRGazeClicker _finalScreenReticleScript;



  private void Start() {
    if (_lapText) {
      _baseLapTemplate = _lapText.text;
    }

    if (_currentCheckpointText) {
      _baseCheckpointTemplate = _currentCheckpointText.text;
    }

    if (_timeText) {
      _baseTimeTemplate = _timeText.text;
    }

    if (_skippedPointImage != null) {
      _skippedPointImage.gameObject.SetActive(false);
      SetImageAlpha(1f);
    }
    _checkpointHandler = FindFirstObjectByType<CheckpointHandler>();
    if (_checkpointHandler == null) {
      Debug.LogWarning("HUDManager: CheckpointHandler reference is missing!");
    }
    CarController.OnCarTelemetryUpdated += UpdateHUD;

    if (_checkpointHandler != null) {
      _checkpointHandler.OnSkippedPoint += ShowSkippedPointWarning;
      _checkpointHandler.OnCheckpointReached += HandleReachedCheckpoint;
      _checkpointHandler.OnLapCompleted += HandleLapCompleted;
      _checkpointHandler.OnRaceStarted += StartTimer;
      _checkpointHandler.OnRaceFinished += ShowRaceFinished;
    }
    CarLoader carLoader = FindObjectsByType<CarLoader>(FindObjectsSortMode.None)[0];
    if (carLoader == null || carLoader.CurrentCar == null) {
      Debug.LogWarning("HUDManager: CarLoader or CurrentCar reference is missing!");
      return;
    }
    _finalScreenReticleCanvas =
      carLoader.CurrentCar.GetComponentInChildren<Canvas>(true);
    if (_finalScreenReticleCanvas != null) {
      _finalScreenReticleCanvas.enabled = false;
    } else {
      Debug.LogWarning("HUDManager: No Canvas found on CurrentCar!");
    }
    _finalScreenReticleScript =
      carLoader.CurrentCar.GetComponentInChildren<VRGazeClicker>();
    if (_finalScreenReticleScript == null) {
      Debug.LogWarning("HUDManager: No VRGazeClicker found on CurrentCar!");
    }
  }

  private void Update() {
    if (_timerIsRunning && _timeText != null) {
      _elapsedTime += Time.deltaTime;
      _timeText.text = FormatTime(_elapsedTime);
    }
  }


  private void OnDisable() {
    CarController.OnCarTelemetryUpdated -= UpdateHUD;

    if (_checkpointHandler != null) {
      _checkpointHandler.OnSkippedPoint -= ShowSkippedPointWarning;
      _checkpointHandler.OnCheckpointReached -= HandleReachedCheckpoint;
      _checkpointHandler.OnLapCompleted -= HandleLapCompleted;
      _checkpointHandler.OnRaceStarted -= StartTimer;
      _checkpointHandler.OnRaceFinished -= ShowRaceFinished;
    }
  }

  private void UpdateHUD(float speed, float rpm, int gear) {
    if (_needleTransform != null) {
      float normalizedSpeed = Mathf.Clamp01(speed / _maxSpeed);
      float angle = Mathf.Lerp(_minNeedleAngle, _maxNeedleAngle, normalizedSpeed);
      _needleTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }
  }


  private void StartTimer() {
    _timerIsRunning = true;
  }

  private string FormatTime(float timeInSeconds) {
    TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
    return _baseTimeTemplate + timeSpan.ToString(@"mm\:ss\:fff");
  }

  private IEnumerator HideWarningAfterDelay() {
    _skippedPointImage.gameObject.SetActive(true);
    SetImageAlpha(1f);
    float holdDuration = Mathf.Max(0f, _skippedPointTextWarningDuration - _fadeDuration);
    if (holdDuration > 0f) {
      yield return new WaitForSeconds(holdDuration);
    }
    float fadeTimer = 0f;
    Color currentColor = _skippedPointImage.color;
    while (fadeTimer < _fadeDuration) {
      fadeTimer += Time.deltaTime;
      float fadeProgress = fadeTimer / _fadeDuration;

      currentColor.a = Mathf.Lerp(1f, 0f, fadeProgress);
      _skippedPointImage.color = currentColor;

      yield return null;
    }

    SetImageAlpha(0f);
    _skippedPointImage.gameObject.SetActive(false);
    _warningCoroutine = null;
  }

  private void SetImageAlpha(float alpha) {
    if (_skippedPointImage == null) {
      return;
    }

    Color imageColor = _skippedPointImage.color;
    imageColor.a = alpha;
    _skippedPointImage.color = imageColor;
  }

  private void ShowSkippedPointWarning() {
    if (_warningCoroutine != null) {
      StopCoroutine(_warningCoroutine);
    }

    _warningCoroutine = StartCoroutine(HideWarningAfterDelay());
  }

  private void ShowRaceFinished() {
    if (_raceFinishedImage != null) {
      _raceFinishedImage.gameObject.SetActive(true);
      _timerIsRunning = false;
      _finalScreenReticleCanvas.gameObject.SetActive(true);
      _finalScreenReticleScript.enabled = true;
    }
    if (_timeText != null) {
      _timeText.color = Color.green;
    }
  }

  private void HandleReachedCheckpoint(int checkpointIndex) {
    if (_warningCoroutine != null) {
      StopCoroutine(_warningCoroutine);
      if (_skippedPointImage) {
        _skippedPointImage.gameObject.SetActive(false);
      }
    }
    if (_currentCheckpointText) {
      _currentCheckpointText.text = FormatCheckpointText(checkpointIndex, _checkpointHandler.TotalCheckpoints);
    }
  }

  private void HandleLapCompleted(int lapNumber) {
    if (_lapText) {
      _lapText.text = $"{_baseLapTemplate} {lapNumber}";
    }
  }

  private string FormatCheckpointText(int current, int total) {
    return $"{_baseCheckpointTemplate} {current}/{total}";
  }

}