using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoQest1 : MonoBehaviour
{
    [SerializeField] GameObject _fadePanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Next());
        }
    }


    IEnumerator Next()
    {
        _fadePanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Qest1");
    }
}
