using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathCubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float tagetNumberChangingDelay = 3f;

    private MathTaskGenerator taskGenerator;
    private TargetNumberUI targetNumberUI;
    private SFXPlayer soundPlayer;

    private List<MathTaskPair> taskPairs = new List<MathTaskPair>();
    private int currentPairIndex = 0;
    private int tasksPerTarget = 23;

    private void Start()
    {
        taskGenerator = GetComponent<MathTaskGenerator>();
        soundPlayer = GetComponent<SFXPlayer>();
        targetNumberUI = FindObjectOfType<TargetNumberUI>();
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            int targetNumber = Random.Range(5, 20);

            targetNumberUI.SetTargetNumber(targetNumber);
            soundPlayer.PlayTargetNumberChangedSFX();

            GenerateTaskPairsForTarget(targetNumber);

            currentPairIndex = 0;
            while (currentPairIndex < taskPairs.Count)
            {
                SpawnPair(taskPairs[currentPairIndex]);
                currentPairIndex++;
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitForSeconds(tagetNumberChangingDelay);
        }
    }

    private void GenerateTaskPairsForTarget(int targetNumber)
    {
        taskPairs.Clear();
        List<MathTaskPair> generatedPairs = taskGenerator.GenerateTaskPairsForTarget(targetNumber);

        for (int i = 0; i < tasksPerTarget && i < generatedPairs.Count; i++)
        {
            taskPairs.Add(generatedPairs[i]);
        }
    }

    private void SpawnPair(MathTaskPair pair)
    {
        GameObject left = SpawnCube(pair.leftTask, leftSpawnPoint);
        GameObject right = SpawnCube(pair.rightTask, rightSpawnPoint);

        MathCube leftMath = left.GetComponent<MathCube>();
        MathCube rightMath = right.GetComponent<MathCube>();

        if (leftMath != null && rightMath != null)
        {
            leftMath.pairedCube = rightMath;
            rightMath.pairedCube = leftMath;
        }
    }

    private GameObject SpawnCube(MathTask task, Transform spawnPoint)
    {
        GameObject cube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);
        MathCube mathCube = cube.GetComponent<MathCube>();
        if (mathCube != null)
        {
            mathCube.SetTask(task);
        }
        return cube;
    }
}
