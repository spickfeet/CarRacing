using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Класс точка входа
/// </summary>
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GhostMovement _ghostMovement;
    [SerializeField] private Finish _finish;
    [SerializeField] private SavePosition _savePosition;
    [SerializeField] private RaceController _raceController;
    [SerializeField] private CinemachineVirtualCamera _cinemachine;
    [SerializeField] private AudioMixerGroup _audioMixer;

    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private TextMeshProUGUI _raceNumberText;
    public void Awake()
    {
        _raceController.Inject(_savePosition, _ghostMovement, _player.transform, _finish, 
            _player.GetComponent<Rigidbody>(), _cinemachine, _pauseMenu, _raceNumberText, _audioMixer);

        _savePosition.Inject(_player.transform);

        _pauseMenu.Inject(_audioMixer);
    }
}
