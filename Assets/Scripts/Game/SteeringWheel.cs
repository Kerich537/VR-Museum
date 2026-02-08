using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField] private Transform _steeringWheel;

    [SerializeField] private float _maxAngleTurn = 90f;

    public void ReturnPosition()
    {
        transform.position = _steeringWheel.position;
        transform.rotation = _steeringWheel.rotation;
    }

    private void FixedUpdate()
    {
        float angleInspector = Mathf.Repeat(gameObject.transform.eulerAngles.x + 180, 360) - 180;
        float ratio = Mathf.Lerp( 
            -1, 1, Mathf.InverseLerp(-_maxAngleTurn, _maxAngleTurn, Mathf.Clamp(angleInspector, -_maxAngleTurn, _maxAngleTurn)) 
            );


    }
}
