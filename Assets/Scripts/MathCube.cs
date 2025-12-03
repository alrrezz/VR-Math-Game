using UnityEngine;
using TMPro;

public class MathCube : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] public MathTask mathTask;

    public bool isHit = false;
    public MathCube pairedCube;

    public void SetTask(MathTask task)
    {
        this.mathTask = task;
        SetMathCubeText(task);
    }

    private void SetMathCubeText(MathTask task)
    {
        textMesh.text = task.GetExpression();
    }
}
