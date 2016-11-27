using UnityEngine;
using System.Collections.Generic;
using NCalc;

public class PathGrapher   {
    public float xValue;
    float deltaX, yTranslation;
    MathFunction function;

    List<Vector3> points;
    public List<Vector3> Points {
        get {
            return points;
        }
    }

    public PathGrapher(float _deltaX = 0.05f) {
        xValue = 0f;
        yTranslation = 0;
        deltaX = _deltaX;
        points = new List<Vector3>();
    }

    public PathGrapher(string _expression, float _deltaX = 0.05f) : this(_deltaX) {
        SetFunction(_expression);
    }

    public void SetFunction(string _expression) {
        function = new MathFunction(_expression);
        xValue = 0f;
        yTranslation = 0f;
    }


    public Vector3 GetNextPosition() {
        float y = function.Evaluate(xValue);
        if (Mathf.Approximately(xValue, 0f) && !Mathf.Approximately(y, 0f)){
            yTranslation = -y;
        }
        if (float.IsInfinity(y)) {
            yTranslation = 0;
            y = 0;
        }
        Vector3 nextPos = new Vector3(xValue, y + yTranslation, 0);
        points.Add(nextPos);
        xValue += deltaX;
        return nextPos;
    }

    class MathFunction {
        Expression expression;

        public MathFunction(string _expression) {
            expression = new Expression(parseEquation(_expression), EvaluateOptions.IgnoreCase);
        }

        public float Evaluate(float x) {
            expression.Parameters["x"] = x;
            float y = float.Parse(expression.Evaluate().ToString());
            return y;
        }

        string parseEquation(string equation) {
            int openIndex, closeIndex;
            while ((openIndex = equation.IndexOf('[')) >= 0
                    && (closeIndex = equation.LastIndexOf(']')) >= 0) {
                replaceExponents(ref equation, openIndex, closeIndex);
            }
            if (equation.Contains("[") || equation.Contains("]"))
                System.Console.Write("ERROR OCCURED! Brackets don't match.");

            return equation;
        }

        void replaceExponents(ref string equation, int start, int end) {
            string newStr;
            newStr = equation.Substring(0, start - 2);
            if (equation[start - 2] == 'x') {
                newStr += "Pow(x," + equation.Substring(start + 1, end - start - 1) + ")"; // x^[e^[3]]
            }
            else {
                newStr += "Exp(" + equation.Substring(start + 1, end - start - 1) + ")";
            }
            newStr += equation.Substring(end + 1);
            equation = newStr;
        }

    }

}


