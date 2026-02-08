using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    private enum AxesEnum { x, y, z }

    [Header("Preferences")]
    [SerializeField] private Transform _target;
    [SerializeField] private AxesEnum _rotationAxe;

    [Header("Settings")]
    [SerializeField] private bool _useBorders;
    [SerializeField] private bool _activatingByAngle;
    [SerializeField] private bool _activatingByTrigger;
    
    [Header("borders")]
    [SerializeField] private float _standardAngle, _maxRotationAngle;
    [SerializeField] private float _activateInfelicity;

    [Header("Events")]
    [SerializeField] private UnityEvent _activateEvent;
    [Tooltip("Event works with activatingByTrigger and activatingByAngle")]
    [SerializeField] private UnityEvent _activatedActionEvent;
    [Tooltip("Event works ONLY with activatingByTrigger")]
    [SerializeField] private UnityEvent _activatedInverseActionEvent;
    private bool _isInvoked;

    private void FixedUpdate()
    {
        RotateToTarget();
        LimitRotation();
        ActivateLeverByAngle();
    }


    private void RotateToTarget()
    {
        Vector3 dir = _target.position - transform.position;

        if (_rotationAxe == AxesEnum.x)
        {
            dir.x = 0;
        }
        else if (_rotationAxe == AxesEnum.y)
        {
            dir.y = 0;
        }
        else if (_rotationAxe == AxesEnum.z)
        {
            dir.z = 0;
        }

        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LimitRotation()
    {
        if (_useBorders)
        {
            if (_rotationAxe == AxesEnum.x)
            {
                float angle = transform.eulerAngles.x;
                angle = Mathf.Clamp(angle, _standardAngle - _maxRotationAngle, _standardAngle + _maxRotationAngle);
                transform.eulerAngles = new Vector3(angle, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else if (_rotationAxe == AxesEnum.y)
            {
                float angle = transform.eulerAngles.y;
                angle = Mathf.Clamp(angle, _standardAngle - _maxRotationAngle, _standardAngle + _maxRotationAngle);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            }
            else if (_rotationAxe == AxesEnum.z)
            {
                float angle = transform.eulerAngles.z;
                angle = Mathf.Clamp(angle, _standardAngle - _maxRotationAngle, _standardAngle + _maxRotationAngle);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
            }
        }
    }

    private void ActivateLeverByAngle()
    {
        if (!_isInvoked && _activatingByAngle)
        {
            if (_rotationAxe == AxesEnum.x &&
                (transform.eulerAngles.x > (_standardAngle + _maxRotationAngle - _activateInfelicity) ||
                transform.eulerAngles.x < (_standardAngle - _maxRotationAngle + _activateInfelicity)))
            {
                _isInvoked = true;
                _activateEvent.Invoke();
                _activatedActionEvent.Invoke();
            }

            if (_rotationAxe == AxesEnum.y &&
                (transform.eulerAngles.y > (_standardAngle + _maxRotationAngle - _activateInfelicity) ||
                transform.eulerAngles.y < (_standardAngle - _maxRotationAngle + _activateInfelicity)))
            {
                _isInvoked = true; 
                _activateEvent.Invoke();
                _activatedActionEvent.Invoke();
            }

            if (_rotationAxe == AxesEnum.z &&
                (transform.eulerAngles.z > (_standardAngle + _maxRotationAngle - _activateInfelicity) ||
                transform.eulerAngles.z < (_standardAngle - _maxRotationAngle + _activateInfelicity)))
            {
                _isInvoked = true; 
                _activateEvent.Invoke();
                _activatedActionEvent.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LeverTrigger leverTrigger) && !_isInvoked && _activatingByTrigger)
        {
            if (leverTrigger.directionIsForward)
            {
                _isInvoked = true;
                _activateEvent.Invoke();
                _activatedActionEvent.Invoke();
            }
            else if (!leverTrigger.directionIsForward)
            {
                _isInvoked = true;
                _activateEvent.Invoke();
                _activatedInverseActionEvent.Invoke();
            }
        }
    }
    public void IsInvokedFalse() { _isInvoked = false; }
}
