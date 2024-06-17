using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<UDPManager>().StartNetworkTask();
        FindObjectOfType<UDPManager>().OnReceivedStringDataEvent.AddListener(async (str) =>
        {
            await UniTask.SwitchToMainThread();
            if(str == "Pause")
            {
                FindObjectOfType<Move>().transform.localScale = Vector3.one * 2;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
