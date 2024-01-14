using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab1;
    [SerializeField]
    private GameObject pipePrefab2;
    [SerializeField]
    private GameObject pipePrefab3;

    [SerializeField]
    private GameObject foodPrefab;
	[SerializeField]
	private GameObject food2Prefab;

	private float pipeSpawnPeriod = 4f; // every 4 seconds
    private float pipeCountdown   = 0f; // countdown to next spawn
    private float foodCountdown   = 0f;

    void Start()
    {
        pipeCountdown = GameState.pipeSpawnPeriod;
        SpawnPipe();
        foodCountdown = pipeCountdown / 2f;
        SpawnFood();
    }

    void Update()
    {
        pipeCountdown -= Time.deltaTime;
		if(pipeCountdown <= 0f)
        {
			pipeCountdown = GameState.pipeSpawnPeriod;
			SpawnPipe();
		}
        foodCountdown -= Time.deltaTime;
        if(foodCountdown <= 0f)
        {
            if(foodCountdown - Time.deltaTime < 0f)
            {
			    SpawnFood();
            }
           foodCountdown = GameState.pipeSpawnPeriod;
        }
    }

    private void SpawnPipe()
    {
        // Take random pipe from 3
        var pipe = GameObject.Instantiate(Random.value < 0.33f ? pipePrefab1 : Random.value < 0.5f ? pipePrefab2 : pipePrefab3);
        pipe.transform.position = this.transform.position + Vector3.up * Random.Range(-1f, 1f);
    }

	private void SpawnFood()
	{
		var food = GameObject.Instantiate(Random.value < 0.5f ? foodPrefab : food2Prefab);
		food.transform.position = this.transform.position + Vector3.up * Random.Range(-3f, 3f);
	}
}
