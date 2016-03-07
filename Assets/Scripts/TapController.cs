using UnityEngine;
using System.Collections;

public class TapController : MonoBehaviour {

    [SerializeField]
    private GameObject weapon;

    public float sensitivityHor = 2.0f; 

    private const float MIN_ANGLE = -0.6f;
    private const float MAX_ANGLE = 0.6f;

    private bool direction; 
    
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = pz.x > 0;
            if (weapon.transform.rotation.z <= MIN_ANGLE && direction)
            {
                return; 
            }
            if (weapon.transform.rotation.z >= MAX_ANGLE && !direction)
            {
                return;
            }
            weapon.transform.Rotate(0, 0, pz.x > 0 ? -sensitivityHor : sensitivityHor); 
        }
    }
   
}
