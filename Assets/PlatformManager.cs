using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

    private Vector3 _NextSpawnPoint = new Vector3(0, 0, 0);

    public GameObject Prefab;
    public float SpawnDistance = 50;
    public float RecycleDistance = 50;

    private Queue<GameObject> _InUse = new Queue<GameObject>();
    private Queue<GameObject> _Available = new Queue<GameObject>();

    public float MinWidth = 5;
    public float MaxWidth = 10;

    public float MinDistance = 10;
    public float MaxDistance = 20;

    public float MinY = -5;
    public float MaxY = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var runnerPos = this.gameObject.transform.position;

        if (_InUse.Count > 0)
        {
            float distanceToOldest = Vector3.Distance(_InUse.Peek().transform.position, runnerPos);

            if (distanceToOldest > RecycleDistance)
            {
                RecycleOldestPlatform();
            }
        }

        float distanceToSpawnPoint = Vector3.Distance(runnerPos, _NextSpawnPoint);

        if (distanceToSpawnPoint < SpawnDistance)
        {
            SpawnNewPlatform();
        }
	}

    private void RecycleOldestPlatform()
    {
        GameObject obj = _InUse.Dequeue();
        obj.SetActive(false);
        _Available.Enqueue(obj);
    }

    private void SpawnNewPlatform()
    {
        GameObject platform;
        if (_Available.Count > 0)
        {
            platform = _Available.Dequeue();
            platform.SetActive(true);
        }
        else
        {
            platform = GameObject.Instantiate(Prefab);
        }

        _InUse.Enqueue(platform);
        MovePlatformToNextLocation(platform);

        
    }

    private void MovePlatformToNextLocation(GameObject platform)
    {
        platform.transform.position = _NextSpawnPoint;
        platform.transform.localScale = new Vector3(Random.Range(MinWidth, MaxWidth), 1, 1);

        var offset = new Vector3(Random.Range(MinDistance, MaxDistance), Random.Range(MinY, MaxY));

        _NextSpawnPoint += offset;
    }
}
