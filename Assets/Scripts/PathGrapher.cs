using UnityEngine;
using System.Collections.Generic;
using NCalc;

public class PathGrapher   {
    public float positionX;
    float deltaX, yTranslation;
    FunctionEvaluator equation;
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
        points = new List<Vector3>();
    }

    public PathGrapher(string _expression, float _deltaX = 0.05f) : this(_deltaX) {
        SetFunction(_expression);
    }

    public void SetFunction(string _expression) {
        equation = new FunctionEvaluator(_expression);
        positionX = 0f;
        yTranslation = 0f;
    }


    public Vector3 GetNextPosition() {
        float y = equation.Evaluate(positionX);
        if (Mathf.Approximately(positionX, 0f) && !Mathf.Approximately(y, 0f)){
            yTranslation = -y;
        }
        if (float.IsInfinity(y)) {
            yTranslation = 0;
            y = 0;
        }
        Vector3 nextPos = new Vector3(positionX, y + yTranslation, 0);
        points.Add(nextPos);
        positionX += deltaX;
        return nextPos;
    }


}
