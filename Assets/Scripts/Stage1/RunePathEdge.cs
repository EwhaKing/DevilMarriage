using UnityEngine;

/// <summary>
/// 두 룬을 잇는 미리 그려진 경로. 플레이어가 지나가면 붉게 빛난다.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class RunePathEdge : MonoBehaviour
{
    [SerializeField] private int runeIndexA;
    [SerializeField] private int runeIndexB;
    [SerializeField] private Color inactiveColor = new Color(0.45f, 0.42f, 0.5f, 0.55f);
    [SerializeField] private Color activeColor = new Color(0.92f, 0.12f, 0.1f, 1f);
    [SerializeField] private float pathWidth = 0.1f;
    [SerializeField] private int sortingOrder = 1;

    private LineRenderer _line;
    private bool _traversed;

    public int RuneIndexA => runeIndexA;
    public int RuneIndexB => runeIndexB;
    public bool IsTraversed => _traversed;

    private void Awake()
    {
        EnsureLine();
    }

    private void EnsureLine()
    {
        if (_line != null)
            return;

        _line = GetComponent<LineRenderer>();
        _line.useWorldSpace = true;
        _line.positionCount = 2;
        _line.startWidth = pathWidth;
        _line.endWidth = pathWidth;
        _line.sortingOrder = sortingOrder;
        if (_line.material == null)
            _line.material = new Material(Shader.Find("Sprites/Default"));
    }

    public void Configure(int indexA, int indexB, Vector3 positionA, Vector3 positionB)
    {
        runeIndexA = indexA;
        runeIndexB = indexB;
        EnsureLine();
        SetPositions(positionA, positionB);
        SetTraversed(false);
    }

    public bool Connects(int runeA, int runeB)
    {
        return (runeIndexA == runeA && runeIndexB == runeB)
            || (runeIndexA == runeB && runeIndexB == runeA);
    }

    public void SetPositions(Vector3 positionA, Vector3 positionB)
    {
        EnsureLine();
        _line.SetPosition(0, positionA);
        _line.SetPosition(1, positionB);
    }

    public void SetTraversed(bool traversed)
    {
        _traversed = traversed;
        EnsureLine();
        var color = traversed ? activeColor : inactiveColor;
        _line.startColor = color;
        _line.endColor = color;
    }
}
