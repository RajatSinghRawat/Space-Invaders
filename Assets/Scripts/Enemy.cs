
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public System.Action killed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Laser"))
        {
            gameObject.SetActive(false);
        }
    }
    
}
