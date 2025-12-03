using System.Collections.Generic;
using UnityEngine;

public class MathTaskGenerator : MonoBehaviour
{
    [Header("Scores")]
    [SerializeField] private int easyScore = 20;
    [SerializeField] private int mediumScore = 24;
    [SerializeField] private int hardScore = 31;
    [SerializeField] private int veryHardScore = 35;

    [Header("NumberOfPairs")]
    [SerializeField] private int easyPairs = 7;
    [SerializeField] private int mediumPairs = 7;
    [SerializeField] private int hardPairs = 7;
    [SerializeField] private int veryHardPairs = 2;

    public List<MathTaskPair> GenerateTaskPairsForTarget(int targetNumber)
    {
        List<MathTaskPair> allPairs = new List<MathTaskPair>();

        allPairs.AddRange(GenerateTaskPairs(easyPairs,
            () => CreateCorrectEasyTask(targetNumber),
            () => CreateWrongEasyTask(targetNumber)));

        allPairs.AddRange(GenerateTaskPairs(mediumPairs,
            () => CreateCorrectMediumTask(targetNumber),
            () => CreateWrongMediumTask(targetNumber)));

        allPairs.AddRange(GenerateTaskPairs(hardPairs,
            () => CreateCorrectHardTask(targetNumber),
            () => CreateWrongHardTask(targetNumber)));

        allPairs.AddRange(GenerateTaskPairs(veryHardPairs,
            () => CreateCorrectVeryHardTask(targetNumber),
            () => CreateWrongVeryHardTask(targetNumber)));

        return allPairs;
    }

    private List<MathTaskPair> GenerateTaskPairs(
        int pairCount,
        System.Func<MathTask> createCorrect,
        System.Func<MathTask> createWrong)
    {
        List<MathTaskPair> list = new List<MathTaskPair>();

        for (int i = 0; i < pairCount; i++)
        {
            MathTask correct = createCorrect();
            MathTask wrong = createWrong();

            bool correctOnLeft = Random.value < 0.5f;

            if (correctOnLeft)
                list.Add(new MathTaskPair(correct, wrong));
            else
                list.Add(new MathTaskPair(wrong, correct));
        }

        return list;
    }

    // ------------------- TASK CREATION --------------------

    private MathTask CreateCorrectEasyTask(int target)
    {
        bool useSub = Random.value < 0.5f;
        int a, b;
        string expr;

        if (useSub)
        {
            a = Random.Range(target + 1, target + 10);
            b = a - target;
            expr = $"{a} - {b}";
        }
        else
        {
            a = Random.Range(1, target);
            b = target - a;
            expr = $"{a} + {b}";
        }

        return new MathTask(expr, true, target, easyScore);
    }

    private MathTask CreateWrongEasyTask(int target)
    {
        bool useSub = Random.value < 0.5f;
        int a, b, result;
        string expr;

        if (useSub)
        {
            do
            {
                a = Random.Range(target + 5, target + 15);
                b = a - target + Random.Range(1, 4);
                result = a - b;
            } while (result == target);

            expr = $"{a} - {b}";
        }
        else
        {
            do
            {
                a = Random.Range(1, target);
                b = a - target + Random.Range(1, 4);
                result = a + b;
            } while (result == target);

            expr = $"{a} + {b}";
        }

        return new MathTask(expr, false, target, easyScore);
    }

    private MathTask CreateCorrectMediumTask(int target)
    {
        bool useDiv = Random.value < 0.5f;
        int a, b;
        string expr;

        if (useDiv && target != 0)
        {
            b = Random.Range(1, 10);
            a = target * b;
            expr = $"{a} / {b}";
        }
        else
        {
            a = Random.Range(1, target + 1);
            if (a == 0 || target % a != 0) a = 1;
            b = target / a;
            expr = $"{a} * {b}";
        }

        return new MathTask(expr, true, target, mediumScore);
    }

    private MathTask CreateWrongMediumTask(int target)
    {
        bool useDiv = Random.value < 0.5f;
        int a, b, result;
        string expr;

        if (useDiv && target != 0)
        {
            do
            {
                b = Random.Range(1, 10);
                a = target * b;
                int offset = Random.Range(1, 3);
                b += offset;
                result = a / b;
            } while (result == target);

            expr = $"{a} / {b}";
        }
        else
        {
            do
            {
                a = Random.Range(1, 12);
                b = Random.Range(1, 12) + Random.Range(1, 3);
                result = a * b;
            } while (result == target);

            expr = $"{a} * {b}";
        }

        return new MathTask(expr, false, target, mediumScore);
    }

    private MathTask CreateCorrectHardTask(int target)
    {
        int a = Random.Range(1, target);
        int b = Random.Range(1, 5);
        int c = target - (a * b);
        string expr = $"{a} * {b} + {c}";
        return new MathTask(expr, true, target, hardScore);
    }

    private MathTask CreateWrongHardTask(int target)
    {
        int a, b, c, result;
        string expr;

        do
        {
            a = Random.Range(1, 10);
            b = Random.Range(1, 5);
            c = Random.Range(1, 10) + Random.Range(1, 4);
            result = a * b + c;
        } while (result == target);

        expr = $"{a} * {b} + {c}";
        return new MathTask(expr, false, target, hardScore);
    }

    private MathTask CreateCorrectVeryHardTask(int target)
    {
        for (int baseNum = 2; baseNum <= 9; baseNum++)
        {
            for (int power = 2; power <= 5; power++)
            {
                int baseResult = (int)Mathf.Pow(baseNum, power);
                int diff = target - baseResult;
                string expr;

                if (diff == 0)
                {
                    expr = $"{baseNum}^{power}";
                    return new MathTask(expr, true, target, veryHardScore);
                }
                else if (diff > 0)
                {
                    expr = $"{baseNum}^{power} + {diff}";
                    return new MathTask(expr, true, target, veryHardScore);
                }
                else
                {
                    expr = $"{baseNum}^{power} - {Mathf.Abs(diff)}";
                    return new MathTask(expr, true, target, veryHardScore);
                }
            }
        }

        return new MathTask("1 + 1", true, 2, veryHardScore);
    }

    private MathTask CreateWrongVeryHardTask(int target)
    {
        int baseNum, power, result;
        string expr;

        do
        {
            baseNum = Random.Range(2, 9);
            power = Random.Range(2, 5) + Random.Range(1, 2);
            result = (int)Mathf.Pow(baseNum, power);
        } while (result == target);

        expr = $"{baseNum}^{power}";
        return new MathTask(expr, false, target, veryHardScore);
    }

    private void Shuffle(List<MathTask> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            MathTask temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}
