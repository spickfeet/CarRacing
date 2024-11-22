using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RaceController : MonoBehaviour
{
    private Finish _finish;
    private SavePosition _savePosition;
    private GhostMovement _ghostMovement;

    private CinemachineVirtualCamera _cinemachine;
    private Transform _player;
    private Rigidbody _playerRB;
    private AudioMixerGroup _audioMixer;

    private PauseMenu _pauseMenu;
    private TextMeshProUGUI _raceNumberText;


    private int _currentRace;
    private Vector3 _basePlayerPos;


    private void Awake()
    {
        _currentRace = 1;
    }

    public void Inject(SavePosition savePosition, GhostMovement ghostMovement, Transform player, 
        Finish finish, Rigidbody playerRB, CinemachineVirtualCamera cinemachine, PauseMenu pauseMenu, 
        TextMeshProUGUI raceNumberText, AudioMixerGroup audioMixer)
    {
        _finish = finish;
        _finish.OnFinished += () => { StartCoroutine(StartNextRace()); };
        _ghostMovement = ghostMovement;
        _player = player;
        _basePlayerPos = _player.transform.position;
        _savePosition = savePosition;
        _playerRB = playerRB;
        _cinemachine = cinemachine;
        _pauseMenu = pauseMenu;
        _raceNumberText = raceNumberText;
        _audioMixer = audioMixer;
    }
    
    private void ResetCarSpeed()
    {
        _playerRB.constraints = RigidbodyConstraints.FreezePosition;
        _playerRB.constraints = RigidbodyConstraints.None;
    }

    private void ActiveGhost()
    {
        _ghostMovement.Inject(_savePosition.Points, _savePosition.Rotations);
        Destroy(_savePosition.gameObject);
        _ghostMovement.gameObject.SetActive(true);
    }

    private void ResetPlayerPos()
    {
        _cinemachine.enabled = false; // Отключаем виртуальную камеру для возможности изменит transform
        _cinemachine.transform.position = new Vector3(_basePlayerPos.x, _basePlayerPos.y, _basePlayerPos.z - 5); // Перемещаем камеру за игрока
        // Возвращаем игрока в базовую позицию
        _player.transform.position = _basePlayerPos;
        _player.rotation = Quaternion.Euler(0, 0, 0);
        _cinemachine.enabled = true; 
    }

    public IEnumerator StartNextRace()
    {
        yield return new WaitForSeconds(1f);
        if (_currentRace == 1)
        {
            ResetCarSpeed();
            ActiveGhost();
            ResetPlayerPos();
            _currentRace++;
            _raceNumberText.text = "Заезд " + _currentRace;
            _pauseMenu.Pause();
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
    }

}
