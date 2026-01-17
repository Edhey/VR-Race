using UnityEngine;
using UnityEngine.Serialization;

public class CheckpointHandler : MonoBehaviour {
  public int TotalCheckpoints { get; private set; }
  public delegate void SkippedPoint();
  public event SkippedPoint OnSkippedPoint;
  public delegate void LapCompleted(int lapNumber);
  public event LapCompleted OnLapCompleted;
  public delegate void CheckpointReached(int checkpointIndex);
  public event CheckpointReached OnCheckpointReached;
  public delegate void RaceStarted();
  public event RaceStarted OnRaceStarted;
  public delegate void RaceFinished();
  public event RaceFinished OnRaceFinished;

  [FormerlySerializedAs("NumberOfLaps")]
  [SerializeField] private int _numberOfLaps;
  public int NumberOfLaps {
    get => _numberOfLaps;
    set => _numberOfLaps = value;
  }

  private void Awake() {
    TotalCheckpoints =
      FindObjectsByType<Checkpoint>(FindObjectsSortMode.None).Length;
  }

  private void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent(out Checkpoint checkpoint)) {
      ValidateCheckpoint(checkpoint.Index);
    }
  }

  private void ValidateCheckpoint(int checkpointIndex) {
    if (checkpointIndex != _nextCheckpointIndex) {
      OnSkippedPoint?.Invoke();
      return;
    }
    if (_nextCheckpointIndex == 0 && _currentLap == 0) {
      OnRaceStarted?.Invoke();
      OnLapCompleted?.Invoke(_currentLap);
    }
    _nextCheckpointIndex++;
    if (_nextCheckpointIndex >= TotalCheckpoints) {
      _nextCheckpointIndex = 0;
    }
    OnCheckpointReached?.Invoke(_nextCheckpointIndex);
    if (_nextCheckpointIndex == 0) {
      _currentLap++;
      OnLapCompleted?.Invoke(_currentLap);
      if (_currentLap >= NumberOfLaps) {
        OnRaceFinished?.Invoke();
      }
    }
  }


  private int _nextCheckpointIndex = 0;
  private int _currentLap = 0;
}
