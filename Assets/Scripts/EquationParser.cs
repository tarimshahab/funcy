using UnityEngine;
using System.Collections;
using NCalc;

public class FunctionEvaluator {
    Expression expression;

    public FunctionEvaluator(string _expression) {
        expression = new Expression(_expression); 
    }

    public float Evaluate(float x) {
        expression.Parameters["x"] = x;
        return (float) expression.Evaluate();
    }

    

}
