using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;
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
        var pipe = GameObject.Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position + Vector3.up * Random.Range(-1f, 1f);
    }

	private void SpawnFood()
	{
		var food = GameObject.Instantiate(Random.value < 0.5f ? foodPrefab : food2Prefab);
		food.transform.position = this.transform.position + Vector3.up * Random.Range(-3.7f, 3.7f);
	}
}
