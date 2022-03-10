using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocher : MonoBehaviour
{
    public GameObject RocherPrefab;
    public float respawnTime;
    private Vector2 screenBounds;

    public float MinRange;
    public float MaxRange;

    // Start is called before the first frame update
    void Start()
    {
        respawnTime = Random.Range(MinRange, MaxRange);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(RocherWave());
    }
    private void spawn()
    {
        respawnTime = Random.Range(MinRange, MaxRange);
        GameObject a = Instantiate(RocherPrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x + 4, screenBounds.x - 4), screenBounds.y + 3);
    }
    IEnumerator RocherWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawn();
        }
    }
}