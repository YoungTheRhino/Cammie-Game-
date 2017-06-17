using UnityEngine;
using System.Collections;

public class SingletonScript : MonoBehaviour {
    public static SingletonScript instance = null;
    
    void Awake () {
        DontDestroyOnLoad(this);
        if (instance == null)

            instance = this;

        else if (instance != this)

            Destroy(gameObject);
    }
	
	
}
