using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BuffDebuff : MonoBehaviour
{
    public float effectTime;
    public static readonly float ACTIVETIME = 10f;

    private float minStartAngle = 250;
    private float maxStartAngle = 290;
    private float moveDegree;
    private Vector2 direction;
    [SerializeField]
    private float speed = 2f;
    private Transform trans;
    [SerializeField]
    private SpriteRenderer icon;

    protected ConfigBuffDebuffRecord cf;

    private void Awake()
    {
        trans = transform;
    }

    private void Start()
    {
        ChangeDir(minStartAngle, maxStartAngle);
        StartCoroutine(CheckCollisionPlayer());
        StartCoroutine(CheckActiveTime());
    }

    private void Update()
    {
        trans.position += new Vector3(direction.x,direction.y,0) * Time.deltaTime * speed;
    }

    private void ChangeDir(float minDegree, float maxDegree)
    {
        moveDegree = UnityEngine.Random.Range(minDegree, maxDegree);
        float radiant = Mathf.Deg2Rad * moveDegree;
        Vector2 d = new Vector2(Mathf.Cos(radiant), Mathf.Sin(radiant));
        direction = d.normalized;
    }

    IEnumerator CheckCollisionPlayer()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.05f);
        while (true)
        {
            Collider2D hit2D = Physics2D.OverlapCircle(trans.position, 0.2f * trans.localScale.x, 1 << 12);
            if (hit2D != null)
            {
                OnEffect();
                Destroy(gameObject);
                break;
            }

            yield return waitForSeconds;

        }
    }

    IEnumerator CheckActiveTime()
    {
        yield return new WaitForSeconds(ACTIVETIME / 2);

        yield return new WaitForSeconds(ACTIVETIME / 2);
        Destroy(gameObject);
    }

    public virtual void Setup(ConfigBuffDebuffRecord cf)
    {
        this.cf = cf;
        this.icon.sprite = SpriteLiblary.instance.GetSpriteByName(cf.Sprite);
    }

    public abstract void OnEffect();

    public abstract void AfterEffect();
}
