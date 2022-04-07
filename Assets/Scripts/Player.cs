
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Projectile laserPrefab;
    public float speed;
    private bool laserActive;
    public int lives=3;
    public Text rem_lives;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        rem_lives.text = "REMAINING LIVES:" + lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (!laserActive)
        {
           Projectile projectile = Instantiate(laserPrefab, transform.position, Quaternion.identity);
           projectile.destroyed += LaserDestroyed;
           laserActive = true;
        }
    }
    private void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Missile")|| collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            lives--;
            rem_lives.text ="REMAINING LIVES:"+ lives.ToString();
            if (lives==0)
            {
                gameObject.SetActive(false);
                SceneManager.LoadScene("Game");
            }
        }
    }
}
