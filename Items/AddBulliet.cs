using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBulliet : MonoBehaviour {

    int bulletadd = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {           
            Messenger<int>.Broadcast(GameEvent.ADD_BULLIET, bulletadd);
            Destroy(this.gameObject);
        }
    }
}
