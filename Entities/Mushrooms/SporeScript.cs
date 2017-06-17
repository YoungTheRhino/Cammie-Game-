using UnityEngine;
using System.Collections;

public class SporeScript : ProjectileBase {

    public int healAmount;
    public bool healSporebool = false;
    
    public override void DownDeflected()
    {
        if (healSporebool == true)
        {
            playervariables.Heal(healAmount);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
