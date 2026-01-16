using UnityEngine;
using Whisper.Utils;
using UnityEngine.UI;
using System;
using Whisper;
using Whisper.Samples; // Asumiendo que tus VoiceCommands están aquí

namespace Assets.Scripts.Voice {
  /// <summary>   
  /// Graba audio, transcribe y envía la orden al objeto activo (Coche o Menú).
  /// </summary>
  public class VoiceManager : MonoBehaviour {
    [Header("Dependencies")]
    [SerializeField] private WhisperManager _whisper;
    [SerializeField] private MicrophoneRecord _microphoneRecord;

    // CAMBIO 1: Ya no es _activeCar, ahora es genérico
    [SerializeField] private GameObject _commandReceiver;

    [Header("VAD Settings (Manos Libres)")]
    [SerializeField] private float _volumeThreshold = 0.02f;
    [SerializeField] private float _silenceDurationToCut = 0.5f;
    [SerializeField] private float _maxRecordDuration = 2.0f;

    [Header("Feedback UI")]
    [SerializeField] private TMPro.TMP_Text _statusText;
    [SerializeField] private Image _volumeMeter;

    [Header("Commands Configuration")]
    [SerializeField] private VoiceCommand[] _availableCommands;

    private void Start() {
      _microphoneRecord.OnRecordStop += OnRecordStop;
      _microphoneRecord.OnVadChanged += OnVadChanged;
      _microphoneRecord.vadStop = true;
      StartListeningLoop();
    }

    // CAMBIO 2: Función genérica para registrar quien recibe las órdenes
    public void RegisterReceiver(GameObject target) {
      _commandReceiver = target;
      // Usamos el nombre del objeto para dar feedback útil
      UpdateUI($"Controlando: {target.name}", Color.white);
    }

    private void StartListeningLoop() {
      if (_microphoneRecord.IsRecording)
        return;
      _microphoneRecord.StartRecord();
      UpdateUI("Esperando orden...", Color.gray);
    }

    private void OnVadChanged(bool isSpeaking) {
      if (isSpeaking) {
        UpdateUI("¡Te escucho!", Color.yellow);
        if (_volumeMeter)
          _volumeMeter.color = Color.green;
      } else {
        if (!_microphoneRecord.IsRecording)
          return;
        UpdateUI("...", Color.gray);
        if (_volumeMeter)
          _volumeMeter.color = Color.white;
      }
    }

    private async void OnRecordStop(AudioChunk recordedAudio) {
      StartListeningLoop(); // Reinicio inmediato

      UpdateUI("Procesando...", Color.cyan);

      WhisperResult result = await _whisper.GetTextAsync(recordedAudio.Data,
        recordedAudio.Frequency, recordedAudio.Channels);

      if (result == null || string.IsNullOrEmpty(result.Result))
        return;

      string text = result.Result;
      UpdateUI($"Oído: {text}", Color.white);
      Console.WriteLine($"[VoiceManager] Transcripción: {text}");

      ProcessCommand(text);
    }

    private void ProcessCommand(string text) {
      // Si no hay nadie registrado (ni coche ni menú), no hacemos nada
      if (_commandReceiver == null) {
        UpdateUI("Error: Nada que controlar", Color.red);
        return;
      }

      string cleanText = text.ToLowerInvariant();
      bool commandFound = false;

      foreach (VoiceCommand command in _availableCommands) {
        foreach (string keyword in command.keywords) {
          if (cleanText.Contains(keyword.ToLowerInvariant())) {

            UpdateUI($"¡COMANDO: {command.name}!", Color.green);

            // CAMBIO 3: Ejecutamos sobre el receptor genérico
            // El propio comando (Boost o Menu) se encargará de ver si el receptor es válido
            command.Execute(_commandReceiver);

            commandFound = true;
            break;
          }
        }
        if (commandFound)
          break;
      }

      if (!commandFound) {
        UpdateUI($"No entendí: {text}", Color.red);
      }
    }

    private void UpdateUI(string msg, Color color) {
      if (_statusText != null) {
        _statusText.text = msg;
        _statusText.color = color;
      }
    }

    private void OnDestroy() {
      if (_microphoneRecord != null) {
        _microphoneRecord.OnRecordStop -= OnRecordStop;
        _microphoneRecord.OnVadChanged -= OnVadChanged;
      }
    }
  }
}