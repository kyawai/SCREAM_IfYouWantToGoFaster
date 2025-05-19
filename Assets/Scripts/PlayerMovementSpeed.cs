using UnityEngine;
using UnityEngine.Splines;

/// <summary>
/// Calculate movement speed and move player
/// </summary>
public class PlayerMovementSpeed : MonoBehaviour
{
    [Tooltip("Spline to move player on")]
    [SerializeField]
    private SplineContainer _spline;

    private AudioVolumeListener _audioVolumeListener;
    private float _speed;
    private float _splineLength;
    private float _distancePercentage;

    private void Start()
    {
        _audioVolumeListener = GetComponent<AudioVolumeListener>();
        _splineLength = _spline.CalculateLength();
    }

    private void Update()
    {
        UpdateSpeed();
        MovePlayer();
    }

    private void UpdateSpeed()
    {
        float volume = _audioVolumeListener.GetMicrophoneVolume();

        _speed = volume switch
        {
            > 0.1f and < 0.24f => 2f,
            >= 0.25f and < 0.49f => 4f,
            >= 0.5f and < 0.74f => 8f,
            >= 0.75f => 15f,
            _ => 0f
        };
    }

    private void MovePlayer()
    {
        _distancePercentage += _speed * Time.deltaTime / _splineLength;

        Vector3 currentPosition = _spline.EvaluatePosition(_distancePercentage);
        transform.position = currentPosition;

        if (_distancePercentage > 1f)
        {
            _distancePercentage = 0f;
        }

        Vector3 nextPosition = _spline.EvaluatePosition(_distancePercentage + 0.05f);
        transform.rotation = Quaternion.LookRotation(nextPosition - currentPosition, transform.up);
    }

}
