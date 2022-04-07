
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Enemy[] prefabs;
    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    public Projectile missilePrefab;
    public float missileAttackRate=1.0f;
    public int amountKilled { get; private set; }
    public int amountAlive => totalInvaders - amountKilled;
    public int totalInvaders => rows * columns;
    public float percentKilled => (float)amountKilled / (float)totalInvaders;
    private Vector3 direction = Vector2.right;
    private void Awake()
    {
       for(int row = 0; row<this.rows;row++)
        {
            float width = (this.columns-1);
            float height =(this.rows-1);
            Vector2 centering = new Vector2(-width/2,-height/2);
            Vector2 rawPosition = new Vector2(centering.x,centering.y+(row*0.75f));
            for(int col=0;col<this.columns;col++)
            {
                Enemy enemy = Instantiate(this.prefabs[row],this.transform);
                enemy.killed += InvaderKilled;
                Vector2 position = rawPosition;
                position.x += col;
                enemy.transform.localPosition = position;
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack),missileAttackRate,missileAttackRate);
    }
    private void Update()
    {
        this.transform.position += direction * this.speed.Evaluate(percentKilled) * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        foreach (Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy)
            { continue; }

            if(direction==Vector3.right&&invader.position.x>=(rightEdge.x-0.5f))
            {
                AdvanceRow();
            }
            else if(direction == Vector3.left && invader.position.x <= (leftEdge.x+0.5f))
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()
    {
        direction.x *= -1f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            { continue; }
            if(Random.value<(1.0f/(float)amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    private void InvaderKilled()
    {
        amountKilled++;
    }
}

