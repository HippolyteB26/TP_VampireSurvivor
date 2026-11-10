using UnityEngine;

public class RatSlimeScript : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int MaxHealth;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private float CooldownAttack;
    [SerializeField] private float Damage;
    private float CurrentHealth;
    [SerializeField] GameObject children;

    [SerializeField] private GameObject target;
    [SerializeField] private BoxCollider Hitbox;
    private float TimeOfActivate = 4624651;
    private bool CanHit;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        int chance = Random.Range(1, 101);
        if (chance <= 25)
        {
            target = GameObject.FindGameObjectWithTag("Tonneau");
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        Vector3 x = target.transform.position - transform.position;
        x.y = 0;
        Vector3 nx = x.normalized;
        rb.linearVelocity = new Vector3(nx.x * speed * Time.deltaTime + transform.position.x, transform.position.y, nx.y * speed * Time.deltaTime + transform.position.z);
        Vector3 DistanceVector = target.transform.position - transform.position;
        float DistanceFloat = Mathf.Sqrt(DistanceVector.z * DistanceVector.z + DistanceVector.x * DistanceVector.x);
        if (DistanceFloat <= range && CanHit)
        {
            Hitbox.enabled = true;
            TimeOfActivate = Time.time;
            CanHit = false;
        }
        if (TimeOfActivate + CooldownAttack > Time.time)
        {
            CanHit = true;
        }

    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Instantiate(children, transform.position, Quaternion.identity);
            Instantiate(children, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}