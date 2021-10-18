using UnityEngine;

public sealed class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody2D>().AddForce(StartingDirection * StartingForceMultiplier);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    // Exposed to Unity Editor.
    [SerializeField] private Vector2 StartingDirection = new Vector2(1.0f, 1.0f);
    [SerializeField] private float StartingForceMultiplier = 1.0f;
}
