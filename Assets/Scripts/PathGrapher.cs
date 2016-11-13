using UnityEngine;
using System.Collections.Generic;
using NCalc;

public class PathGrapher   {
    public float positionX;
    float deltaX;
    Expression expression;
    float yTranslation;

    List<Vector3> points;
    public List<Vector3> Points {
        get {
            return points;
        }
    }

    public PathGrapher(float _deltaX = 0.05f) {
        positionX = 0f;
        yTranslation = 0;
        deltaX = _deltaX;
    }

    public PathGrapher(string _expression, float _deltaX = 0.05f) : this(_deltaX) {
        SetFunction(_expression);
    }


    /* This constructor pre-calculates a bunch of point */
    public PathGrapher(float start, float end, float delta) {
        deltaX = delta;
        int pointsCount = (int)(Mathf.Abs(end - start) / deltaX);
        Debug.Log("POINTS COUNT: " + pointsCount);
        points = new List<Vector3>(pointsCount);

        float x = 0f;
        for (int i = 0; i < pointsCount; i++) {
            points.Insert(i, new Vector3(x, EvaluateFunction(x), 0));
            x += deltaX;
        }
    }

    public void SetFunction(string _expression) {
        expression = new Expression(_expression, EvaluateOptions.IgnoreCase);
        positionX = 0f;
        yTranslation = 0f;
    }

    private float EvaluateFunction(float x) {
        expression.Parameters["x"] = x;
        float y = float.Parse(expression.Evaluate().ToString());
        Debug.Log("(" + x + "," + y + ")");
        return y;
    }

    public Vector3 GetNextPosition() {
        float y = EvaluateFunction(positionX);
        if (Mathf.Approximately(positionX, 0f) && !Mathf.Approximately(y, 0f)){
            yTranslation = -y;
        }
        if (float.IsInfinity(y)) {
            yTranslation = 0;
            y = 0;
        }
        Vector3 nextPos = new Vector3(positionX, y + yTranslation, 0);
        positionX += deltaX;
        return nextPos;
    }


}
