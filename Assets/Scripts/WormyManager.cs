using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormyManager : MonoBehaviour
{
    Wormy[] wormies;
    public Transform wormyCamera;

    public static WormyManager singleton;

    // public PhotonView photonView;

    public GameObject WormPrefab;

    public GameObject GameCanvas;

    private int currentWormy;

    private void Awake()
    {
        GameCanvas.SetActive(true);
    //     if (photonView.isMine)
    //     {
    //         wormyCamera.SetActive(true);
    //     }
    }

    void Start()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;

        wormies = GameObject.FindObjectsOfType<Wormy>();
        wormyCamera = Camera.main.transform;

        /* for (int i = 0; i < wormies.Length; i++)
        {
            wormies[i].wormId = i;
        } */

    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-1f, 1f);

        PhotonNetwork.Instantiate(WormPrefab.name, new Vector2(this.transform.position.x * randomValue, 
        this.transform.position.y), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        // wormyCamera.SetActive(false);
    }

    /* public void NextWorm()
    {
        StartCoroutine(NextWormCoroutine());
    } */

   /* public IEnumerator NextWormCoroutine()
    {
        // wormies = GameObject.FindObjectsOfType<Wormy>();
        // wormyCamera = Camera.main.transform;

        for (int i = 0; i < wormies.Length; i++)
        {
            wormies[i].wormId = i;
        }

        var nextWorm = currentWormy + 1;
        currentWormy = -1;

        yield return new WaitForSeconds(2);

        currentWormy = nextWorm;
        if (currentWormy >= wormies.Length)
        {
            currentWormy = 0;
        }

        // wormyCamera.SetParent(wormies[currentWormy].transform);
        // wormyCamera.localPosition = Vector3.zero + Vector3.back * 10;
    } */


   /* public bool IsMyTurn(int i)
    {
        return i == currentWormy;
    } */

}
