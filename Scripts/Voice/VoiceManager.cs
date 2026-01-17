using System;
using UnityEngine;
using UnityEngine.UI;
using Whisper;
using Whisper.Samples; // Asumiendo que tus VoiceCommands están aquí
using Whisper.Utils;

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
      if (_microphoneRecord.IsRecording) {
        return;
      }

      _microphoneRecord.StartRecord();
      UpdateUI("Esperando orden...", Color.gray);
    }

    private void OnVadChanged(bool isSpeaking) {
      if (isSpeaking) {
        UpdateUI("¡Te escucho!", Color.yellow);
        if (_volumeMeter) {
          _volumeMeter.color = Color.green;
        }
      } else {
        if (!_microphoneRecord.IsRecording) {
          return;
        }

        UpdateUI("...", Color.gray);
        if (_volumeMeter) {
          _volumeMeter.color = Color.white;
        }
      }
    }

    private async void OnRecordStop(AudioChunk recordedAudio) {
      WhisperResult result = await _whisper.GetTextAsync(recordedAudio.Data,
          recordedAudio.Frequency, recordedAudio.Channels);

      UnityMainThreadDispatcher(result);
    }

    private void UnityMainThreadDispatcher(WhisperResult result) {

      System.Threading.SynchronizationContext.Current.Post(_ => {
        StartListeningLoop();

        if (result == null || string.IsNullOrEmpty(result.Result)) {
          UpdateUI("No se detectó voz", Color.gray);
          return;
        }

        string text = result.Result;
        UpdateUI($"Oído: {text}", Color.white);
        ProcessCommand(text);

      }, null);
    }

    private void ProcessCommand(string text) {
      if (_commandReceiver == null) {
        UpdateUI("Error: Nada que controlar", Color.red);
        return;
      }

      string cleanText = text.ToLowerInvariant();
      bool commandFound = false;

      foreach (VoiceCommand command in _availableCommands) {
        foreach (string keyword in command.Keywords) {
          if (cleanText.Contains(keyword.ToLowerInvariant())) {

            UpdateUI($"¡COMANDO: {command.name}!", Color.green);

            command.Execute(_commandReceiver);
            commandFound = true;
            break;
          }
        }
        if (commandFound) {
          break;
        }
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