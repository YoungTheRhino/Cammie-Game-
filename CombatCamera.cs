using UnityEngine;
using System.Collections;

public class CombatCamera : MonoBehaviour {

	
	void Start ()
    {
	    
	}
	
	public IEnumerator ShakeByTime(float duration, float shakeAmount)
    {
        Vector2 initialPos = transform.position;
        Vector3 Z = new Vector3(0, 0, -10);
        float timer = 0;
        while (timer < duration)
        {
            transform.position = initialPos + Random.insideUnitCircle * shakeAmount;
            transform.position += Z;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(initialPos.x, initialPos.y, -10);
    }

	void Update ()
    {
	
	}

    void PlayerDeath()
    {
        
    }
}
