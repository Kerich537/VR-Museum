using TMPro;
using UnityEngine;
public class SteeringWheel : MonoBehaviour
{
    [SerializeField] private Transform _steeringWheel;

    [SerializeField] private float _maxTurnAngle;

    [SerializeField] private SteeringWheelTest _steeringWheelScript;

    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        ReturnPosition();
    }

    public void ReturnPosition()
    {
        transform.position = _steeringWheel.position;
        transform.rotation = _steeringWheel.rotation;
    }

    private void FixedUpdate()
    {
        float angle = Mathf.Repeat(gameObject.transform.localEulerAngles.x + 180, 360) - 180; print(angle);
        float ratio = Mathf.Lerp(-1, 1, Mathf.InverseLerp(-_maxTurnAngle, _maxTurnAngle, Mathf.Clamp(angle, -_maxTurnAngle, _maxTurnAngle)) );
        

        /*float angle = transform.eulerAngles.x; print (angle);
        angle = Mathf.Clamp(angle, -_maxTurnAngle, _maxTurnAngle);

        float ratio = angle / _maxTurnAngle;

        angle += 180f;
        
        //print(angle + " " + ratio);*/
        _text.text = (int)angle + "  " + (int)(ratio*100); 
        _steeringWheelScript.RotateSteeringWheel(ratio);
    }
    /*[SerializeField] private SteeringWheelTest _steeringWheelScript;
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private Vector3 _rotationAxis = Vector3.up; // Ось вращения (можно изменить в инспекторе)
    
    private Quaternion _initialLocalRotation;
    
    private void Update()
    {
        Quaternion currentRotation = transform.rotation;
        
        float currentAngle = GetRotationAngle(currentRotation, _rotationAxis);
        
        float limitedAngle = Mathf.Clamp(currentAngle, -_maxRotationAngle, _maxRotationAngle);
        
        Quaternion limitedRotation = Quaternion.AngleAxis(limitedAngle, _rotationAxis);

        _steeringWheelScript.RotateSteeringWheel(limitedAngle);
    }
    
    private float GetRotationAngle(Quaternion rotation, Vector3 axis)
    {
        Vector3 rotationEuler = rotation.eulerAngles;
        
        float angle = 0;
        if (axis == Vector3.right || axis == Vector3.left) angle = NormalizeAngle(rotationEuler.x);
        else if (axis == Vector3.up || axis == Vector3.down) angle = NormalizeAngle(rotationEuler.y);
        else if (axis == Vector3.forward || axis == Vector3.back) angle = NormalizeAngle(rotationEuler.z);
        
        return angle;
    }

    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        if (angle < -180) angle += 360;
        return angle;
    }*/
}
