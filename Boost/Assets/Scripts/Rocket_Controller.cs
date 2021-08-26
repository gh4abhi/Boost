using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_Controller : MonoBehaviour
{
    // todo - We have to fix a lighting bug. 
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField]
    float rcsThrust = 100f;
    [SerializeField]
    float mainThrust = 100f;

    enum State { Alive, Dying, Transcending };

    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();
        }
    }

    // todo fix sound bug.

    void OnCollisionEnter(Collision collision)
    {
        if(state!=State.Alive)
        {
            return;
        }
         switch(collision.gameObject.tag)
        {
            case "Safe":
                print("Ok");
                break;
            case "Obstacles":
                state = State.Dying;
                print("No Fuel");
                Invoke("LoadSceneOne", 1f);
                break;
            case "Fuel" :
                print("Fuel Up");
                break;
            case "Finish":
                print("You Win");
                state = State.Transcending;
                Invoke("LoadNextScene",1f);
                break;
            default:
                print("Not Known");
                break;
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(1); // todo for more than 1 level.
    }
    void LoadSceneOne()
    {
        SceneManager.LoadScene(0); // todo for more than 1 level.
    }

    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))      // We can thrust while rotating.                
        {
            rigidBody.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    void Rotate()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward*rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }


}
