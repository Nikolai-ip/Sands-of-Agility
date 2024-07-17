using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TrajectorySystem
{
    public class TrajectoryMover:MonoBehaviour
    {
        [SerializeField] private List<TrajectoryCurve> _trajectories;
        [SerializeField] private List<AnimationCurve> _speedCurves;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _movable;

        private void OnValidate()
        {
            if (_speedCurves.Count != _trajectories.Count)
            {
                int dif = _trajectories.Count - _speedCurves.Count;
                if (dif > 0)
                    for (int i = 0; i < dif; i++)
                        _speedCurves.Add(new AnimationCurve());
                else
                    _speedCurves.RemoveRange(_speedCurves.Count + dif, -dif);
            }
        }

        public void StartMove()
        {
            StopAllCoroutines();
            StartCoroutine(MoveCoroutine(_movable));
        }
        
        private IEnumerator MoveCoroutine(Transform movable)
        {
            for (var index = 0; index < _trajectories.Count; index++)
            {
                var trajectoryCurve = _trajectories[index];
                var speedCurve = _speedCurves[index];
                yield return MoveByCurve(trajectoryCurve, movable, speedCurve);
            }
        }

        private IEnumerator MoveByCurve(IEnumerable<Vector2> curve, Transform movable, AnimationCurve speedCurve)
        {
            for (int i = 0; i < curve.Count()-1; i++)
            {
                var startPoint = curve.ElementAt(i);
                var endPoint = curve.ElementAt(i + 1);
                float speed = _speed * speedCurve.Evaluate((float)i / curve.Count());
                yield return LinearMove(startPoint, endPoint, movable, speed);
            }
        }

        private IEnumerator LinearMove(Vector2 a, Vector2 b, Transform movable, float speed)
        {
            var delay = new WaitForFixedUpdate();
            float time = 0;
            while (Vector2.Distance(movable.position,b)>0.01f)
            {
                var newPosition = Vector2.Lerp(a, b, time);
                movable.position = newPosition;
                time += Time.deltaTime * speed;
                time = Mathf.Clamp01(time);
                yield return delay;
            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(20, 20, 100, 20), "Start Move"))
            {
                StartMove();
            }
        }
    }
}