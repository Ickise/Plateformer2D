using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject CoinPrefab;
    public float respawnTime;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        respawnTime = Random.Range(5, 10);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(CoinWave());
    }
    private void spawn()
    {
        respawnTime = Random.Range(5, 10);
        GameObject a = Instantiate(CoinPrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x +4, screenBounds.x -4), screenBounds.y +3);
    }
    IEnumerator CoinWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawn();
        }
    }
}
