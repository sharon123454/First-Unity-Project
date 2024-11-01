using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    GameManager gameManager;
    public ParticleSystem particle;
    public float timeReduce;
    [SerializeField]
    string tagToKill = "Target";

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            particle.Play();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(tagToKill))
        {
            gameManager.AddToTimer(timeReduce);
            other.SetActive(false);
        }
    }
}
