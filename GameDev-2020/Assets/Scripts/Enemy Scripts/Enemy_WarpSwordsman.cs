using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WarpSwordsman : MonoBehaviour, IHitable
{
    protected const float activeDist = 12.0f;
    protected const float attackDist = 2.0f;
    protected const float yLimit = 3.0f; //limit for activity
    protected const float shiftTime = 0.5f; //time for shifting
    protected const float attackTime = 0.5f; //active attack time
    protected const float endLag = 1.0f; //lag on attack
    protected const float jumpTime = 0.25f; //Time the actor can jump

    [SerializeField]
    protected GeneralChecker floor, wall, roof;

    [SerializeField]
    protected GameObject baseAttack;

    protected bool acting = false;
    protected bool traveling = false;

    protected Coroutine action = null;

    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    protected Animator animator;

    [SerializeField]
    protected int maxHP = 5;
    [SerializeField]
    protected int HP = 5; 

    [SerializeField]
    protected float speed = 3;

    private bool SameDimension {
        get {
            return(
                (FindObjectOfType<PlayerMovement>() != null) &&
                (
                (FindObjectOfType<PlayerMovement>().gameObject.GetComponent<DimensionManager>().getDimensionID() == 1 && gameObject.layer == 8) ||
                (FindObjectOfType<PlayerMovement>().gameObject.GetComponent<DimensionManager>().getDimensionID() == 2 && gameObject.layer == 9)
                )
                );
        }
    }

    public bool NearPlayer
    {
        get
        {
            return (
                (FindObjectOfType<PlayerMovement>() != null) &&
                (Mathf.Abs(Vector3.Distance(transform.position, GameObject.FindObjectOfType<PlayerMovement>().transform.position)) < activeDist) &&
                (Mathf.Abs(GameObject.FindObjectOfType<PlayerMovement>().transform.position.y - transform.position.y) < yLimit)
                );
        }
    }

    public bool InAttackingDistance {
        get {
            return (
                (FindObjectOfType<PlayerMovement>() != null) &&
                (Mathf.Abs(GameObject.FindObjectOfType<PlayerMovement>().transform.position.x - transform.position.x) <= attackDist) &&
                (Mathf.Abs(GameObject.FindObjectOfType<PlayerMovement>().transform.position.y - transform.position.y) < 1.5f)
                );
        }
    }

    public int Direction {
        get {
            if (FindObjectOfType<PlayerMovement>().transform.position.x - transform.position.x > attackDist && NearPlayer)
            {
                return 1; //Go Right
            }
            else if (FindObjectOfType<PlayerMovement>().transform.position.x - transform.position.x < attackDist && NearPlayer)
            {
                return -1; //Go Left
            }
            else {
                return 0;
            }
        }
    }

    public void Hit(int damage)
    {
        HP -= damage;
        if (HP <= 0) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!NearPlayer || acting) {
            return;
        }

        if (!acting)
        {
            switch (FindObjectOfType<PlayerMovement>().transform.position.x >= transform.position.x)
            {
                case true:
                    transform.localScale = new Vector3(1, transform.localScale.y);
                    break;
                case false:
                    transform.localScale = new Vector3(-1, transform.localScale.y);
                    break;
            }
        }

        SpriteUpdate();

        if (InAttackingDistance) {
            //Attack Player
            traveling = false;

            if (!SameDimension && floor.Grounded) {
                //shift to player dimension
                action = StartCoroutine(Shift());
                return;
            }

            action = StartCoroutine(Attack());

            return;
        }

        //Move towards player
        if (SameDimension)
        {
            //shift away from player dimension
            action = StartCoroutine(Shift());
            return;
        }

        rb.velocity = new Vector2( speed * Direction , rb.velocity.y);

        if (wall.Grounded && floor.Grounded) {
            action = StartCoroutine(Jump());
        }
    }

    public void SpriteUpdate() {
        float a = 1;

        if (!SameDimension) {
            a = 0.1f;
        }

        sprite.color = new Color( 1, 1, 1, a);
    }

    public IEnumerator Attack() {
        acting = true;

        float t = 0;

        //TODO : Instantiate Hitbox and start animation
        Debug.Log(gameObject.name + " I, WarpSword! Attack!");
        while (t < attackTime)
        {
            t += Time.fixedUnscaledDeltaTime * Time.timeScale;
            yield return new WaitForFixedUpdate();
        }

        //TODO : Disentegrate Hitbox

        //Endlag for opening
        while (t < endLag)
        {
            t += Time.fixedUnscaledDeltaTime * Time.timeScale;
            yield return new WaitForFixedUpdate();
        }

        acting = false;
        yield break;
    }

    public IEnumerator Shift() {
        acting = true;

        rb.velocity = Vector2.zero;

        float t = 0;

        while (t < shiftTime) {
            t += Time.fixedUnscaledDeltaTime * Time.timeScale;
            yield return new WaitForFixedUpdate();
        }

        if (gameObject.layer == 8)
        {
            gameObject.layer = 9;
        }
        else {
            gameObject.layer = 8;
        }

        acting = false;

        yield break;
    }

    public IEnumerator Jump() {
        acting = true;
        float t = 0;

        while (t < jumpTime)
        {
            if (roof.Grounded) {
                rb.velocity = new Vector2(speed * Direction, rb.velocity.y / 10);
                break;
            }

            rb.velocity = new Vector2(speed * transform.localScale.x, 10);
            t += Time.fixedUnscaledDeltaTime * Time.timeScale;
            yield return new WaitForFixedUpdate();
        }

        rb.velocity = new Vector2(speed * Direction, rb.velocity.y / 10);

        acting = false;
        yield break;
    }
}
