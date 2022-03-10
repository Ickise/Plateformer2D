using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public Animator anim;

    float timer;

    float RandomAttack;

    public AudioSource DeathSound;

    public GameObject Spawner;
    public GameObject SuccesSound;
    // Start is called before the first frame update
    void Start()
    {
        RandomAttack = Random.Range(7, 15);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= RandomAttack)
        {
            anim.SetBool("IsAttack", true);
            StartCoroutine(Reset());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "death")
        {
            Spawner.SetActive(false);
            DeathSound.Play();
            Destroy(other.gameObject);
            anim.SetBool("IsDead", true);
            
            StartCoroutine(Win());
            StartCoroutine(WinWait());
        }
    }


    IEnumerator Reset()
    {

        timer = 0;
        yield return new WaitForSeconds(0.5f);
        RandomAttack = Random.Range(10, 20);
        timer = 0;
        anim.SetBool("IsAttack", false);
    }

    IEnumerator WinWait()
    {

        yield return new WaitForSeconds(0.5f);
        SuccesSound.SetActive(true);
    }

    IEnumerator Win()
    {

        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
        
        SceneManager.LoadScene("Scenes/Game");
    }
}
