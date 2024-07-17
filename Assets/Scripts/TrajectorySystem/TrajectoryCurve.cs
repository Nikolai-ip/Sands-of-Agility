using System.Collections;
using System.Collections.Generic;
using TrajectorySystem;
using UnityEngine;

public class TrajectoryCurve : MonoBehaviour, IEnumerable<Vector2>
{
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Vector2 _startPoint;
    [SerializeField] private Vector2 _endPoint;
    [SerializeField] private float _radiusPoints;
    [SerializeField] private Color _pointsColor;
    private List<Vector2> _curve = new();
    private CurveCalculator _curveCalculator = new();
    private const float STEP = 0.15f;
    private void OnValidate()
    {
        if (_curve.Count!=0)
            _curve.Clear();
        _curve = _curveCalculator.GetCurvePoints(_startPoint, _endPoint, _animationCurve, STEP);
    }

    private void OnDrawGizmos()
    {
        if (_curve.Count == 0) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_curve[0],_radiusPoints*1.5f);
        Gizmos.color = _pointsColor;
        for (int i = 1; i < _curve.Count-1; i++)
        {
            Gizmos.DrawWireSphere(_curve[i],_radiusPoints);
        }
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_curve[^1],_radiusPoints*1.5f);


    }
    
    public IEnumerator<Vector2> GetEnumerator()
    {
        return _curve.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
