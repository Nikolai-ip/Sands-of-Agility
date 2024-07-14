using System.Collections.Generic;
using UnityEngine;

namespace TrajectorySystem
{
    public class CurveCalculator
    {
        public List<Vector2> GetCurvePoints(Vector2 startPoint, Vector2 endPoint, AnimationCurve curve, float step)
        {
            float distance = Vector2.Distance(startPoint, endPoint);
            Vector2 dir = (endPoint - startPoint).normalized;
            List<Vector2> points = new();
            for (float i = 0; i < distance; i+=step)
            {
                Vector2 point = startPoint + dir * i;
                points.Add(new Vector2(point.x,point.y+curve.Evaluate(i/distance)));
            }

            return points;
        }
    }
}