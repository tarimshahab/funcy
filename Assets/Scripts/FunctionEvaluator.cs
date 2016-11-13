using NCalc;

public class FunctionEvaluator {
    Expression expression;

    public FunctionEvaluator(string _expression) {
        expression = new Expression(parseEquation(_expression), EvaluateOptions.IgnoreCase); 
    }

    public float Evaluate(float x) {
        expression.Parameters["x"] = x;
        float y = float.Parse(expression.Evaluate().ToString());
        return y;
    }

    private string parseEquation(string equation) {
        int openIndex, closeIndex;
        while((openIndex = equation.IndexOf('[')) >= 0
                && (closeIndex = equation.LastIndexOf(']')) >= 0) {
            replaceExponents(ref equation, openIndex, closeIndex);
        }
        if (equation.Contains("[") || equation.Contains("]"))
            System.Console.Write("ERROR OCCURED! Brackets don't match.");

        return equation;        
    }

    private void replaceExponents(ref string equation, int start, int end) {
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
