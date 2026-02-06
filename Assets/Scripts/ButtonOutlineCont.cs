using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class ButtonOutlineCont : MonoBehaviour
{
    [SerializeField] private Color _normalColor, _pressedColor;
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void ChangeColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0:
                _outline.OutlineColor = _normalColor; break;
            case 1:
                _outline.OutlineColor = _pressedColor; break;
        }
    }

    public void SetOutline(bool isEnabled) { _outline.enabled = isEnabled; }
}
