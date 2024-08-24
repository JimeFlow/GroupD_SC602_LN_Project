using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinText;
    public GameObject blockingObject;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        coinText.text = ": " + coinCount.ToString();

        if(coinCount == 10) //si consigue 10 monedas 
        {
            Destroy(blockingObject);
            SoundManager.Instance.PlaySFX("TreeUnblockingSFX" ,false);
        }
    }
}
