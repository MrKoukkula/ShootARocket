using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigitBody;
    [SerializeField] float turningSpeed = 5f;
    [SerializeField] float thrustSpeed = 10f;
    [SerializeField] int nextLevel;
    AudioSource rocketThrustSound;
    enum State { alive, dead, levelingUp };

    State state = State.alive;



    // Start is called before the first frame update
    void Start()
    {
        rocketRigitBody = GetComponent<Rigidbody>();
        rocketThrustSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            Thrust();
            Rotate();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( state != State.alive)
        {
            return;
        }

        if (collision.gameObject.tag == "friendly")
        {
            Debug.Log("ok");
        } else if (collision.gameObject.tag == "goal")
        {
            state = State.levelingUp;
            Invoke("loadScenes", 2f);
            Debug.Log("You win!");
        } else
        {
            state = State.dead;
            Invoke("playerDeath", 2f);
            Debug.Log("You died");
        }
    }

    void loadScenes()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void playerDeath()
    {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rocketThrustSound.volume = 1.0f;
            rocketThrustSound.Play();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rocketRigitBody.AddRelativeForce(Vector3.up * (Time.deltaTime * thrustSpeed));
        }
        else
        {
            rocketThrustSound.volume = rocketThrustSound.volume - 0.1f;
            if (rocketThrustSound.volume == 0)
            {
                rocketThrustSound.Stop();
            }
        }
    }

    private void Rotate()
    {

        rocketRigitBody.freezeRotation = true; // stop environment physics rotation

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * (Time.deltaTime * turningSpeed));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //print("Turning right");
            transform.Rotate(-Vector3.forward * (Time.deltaTime * turningSpeed));
        }

        rocketRigitBody.freezeRotation = false; // continue environment physics rotation
    }
}
