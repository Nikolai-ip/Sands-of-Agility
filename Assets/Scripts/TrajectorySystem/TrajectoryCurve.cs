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
    [SerializeField] private float _step;
    private void OnValidate()
    {
        if (_curve.Count!=0)
            _curve.Clear();
        _curve = _curveCalculator.GetCurvePoints(_startPoint, _endPoint, _animationCurve, _step);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _pointsColor;
        if (_curve.Count == 0) return;
        for (int i = 0; i < _curve.Count; i++)
        {
            Gizmos.DrawWireSphere(_curve[i],_radiusPoints);
        }
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
