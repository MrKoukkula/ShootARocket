using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigitBody;
    [SerializeField] float turningSpeed = 5f;
    [SerializeField] float thrustSpeed = 10f;
    //[SerializeField] int nextLevel;
    AudioSource audioPlayer;
    [SerializeField] AudioClip rocketThrust;
    [SerializeField] AudioClip rocketDeath;
    [SerializeField] AudioClip rocketWin;
    [SerializeField] float levelLoadDelay;
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem winParticles;
    [SerializeField] ParticleSystem deathParticles;
    bool collisionsEnabled = true;

    enum State { alive, dead, levelingUp };

    State state = State.alive;



    // Start is called before the first frame update
    void Start()
    {
        rocketRigitBody = GetComponent<Rigidbody>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            Thrust();
            Rotate();
        }

        if (Debug.isDebugBuild)
        {
            respondToDebugKeys();
        }
        
        
    }

    private void respondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            loadScenes();
        } else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsEnabled = !collisionsEnabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ( state != State.alive || !collisionsEnabled)
        {
            return;
        }

        if (collision.gameObject.tag == "friendly")
        {
            Debug.Log("ok");
        } else if (collision.gameObject.tag == "goal")
        {
            state = State.levelingUp;
            Invoke("loadScenes", levelLoadDelay);
            Debug.Log("You win!");
            audioPlayer.Stop();
            thrustParticles.Stop();
            winParticles.Play();
            audioPlayer.PlayOneShot(rocketWin);
        } else
        {
            state = State.dead;
            Invoke("playerDeath", levelLoadDelay);
            Debug.Log("You died");
            audioPlayer.Stop();
            thrustParticles.Stop();
            deathParticles.Play();
            audioPlayer.PlayOneShot(rocketDeath);
        }
    }

    void loadScenes()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        } else
        {
            SceneManager.LoadScene(0);
        }
        
    }

    void playerDeath()
    {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {

        // Apply force to rigid body
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRigitBody.AddRelativeForce(Vector3.up * (Time.deltaTime * thrustSpeed));
            if (!audioPlayer.isPlaying)
            {
                audioPlayer.PlayOneShot(rocketThrust);
            }
            thrustParticles.Play();
        }
        else
        {
            if (state != State.dead && state != State.levelingUp)
            {
                audioPlayer.Stop();
                thrustParticles.Stop();
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
