using UnityEngine;

public class BallManager : MonoBehaviour
{
    [HideInInspector] public bool Dead=true;
    [HideInInspector] public int DifficultLevel;
    private GameObject ball;
    private float startSpeed = 5.0f;
    private float gravity = 1f;
    private Vector2 velocity;
    public bool Pressed;
    private int verticalSpeedModifier=1;
    private float nextTime = 0.0f;
    private float modifier = 0f;
    private float lastTime = 0f;
    private float startTime = 0f;
    private Vector2 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            Movement(velocity);
        }
    }

    public void PressedDown()
    {
        Pressed = true;
    }
    public void PressedUp()
    {
        Pressed = false;
    }

    public void Restart()
    {
        if (TryGetComponent(out GameManager gameManager))
        {
            Pressed = false;
            startTime = Time.time;
            gameManager.Attempts++;
            gameManager.GameMenu.SetActive(true);
            lastPosition = ball.transform.position;
            Dead = false;
            verticalSpeedModifier = 1;
            ball.transform.position = lastPosition * new Vector2(1, 0);
            SetStartSpeed(DifficultLevel);
        }
    }

    public void StartGame()
    {
        Initialization();
    }

    private void Movement(Vector2 _velocity)
    {
        SpeedControl();
        if (Pressed)
        {
            gravity = verticalSpeedModifier;
        }
        else
        {
            gravity = -verticalSpeedModifier;
        }
        SetVelocity();
        if (ball != null)
        {
            ball.transform.Translate(_velocity);
            Camera.main.transform.Translate(_velocity * Vector2.right);
        }
        
    }

    private void SetVelocity()
    {
       velocity = new Vector2(startSpeed * Time.deltaTime, gravity * Time.deltaTime);
    }

    private void SpeedControl()
    {
        if (modifier == 0)
        {
            modifier = 15;
            nextTime = Time.time + modifier;
        }

        if (Time.time > nextTime)
        { 
            verticalSpeedModifier++;
            modifier = 0;
        }
    }

    private void OnGUI()
    {
        
       
    }

    private void CreateBall()
    {
        ball = Instantiate(Resources.Load("Prefabs\\BallPrefab") as GameObject, Vector3.zero, Quaternion.identity);
        ball.AddComponent<CollisionCheck>();
        Dead = false;
    }

    public void SetStartSpeed(int _DifficultLevel)
    {
        switch (_DifficultLevel)
        {
            case 1:
                startSpeed = 5.0f;
                break;
            case 2:
                startSpeed = 8.0f;
                break;
            case 3:
                startSpeed = 10.0f;
                break;
        }
    }

    private void Initialization()
    {
        if (TryGetComponent(out GameManager gameManager))
        {
            startTime = Time.time;
            gameManager.Attempts++;
            SetStartSpeed(DifficultLevel);
            CreateBall();
        }
    }
    private float CalculateTime(float _lastTime,float _startTime)
    {
        return _lastTime - _startTime;
    }
    public void GameOver()
    {
        if (TryGetComponent(out GameManager gameManager))
        {        
            lastTime = Time.time;
            var endTime = CalculateTime(lastTime, startTime).ToString();
            Dead = true;
            gameManager.GameMenu.SetActive(false);
            gameManager.DifficultPanel.SetActive(false);
            gameManager.GameOverMenu.SetActive(true);
            gameManager.Attempt.text = "Попытка: " + gameManager.Attempts;
            gameManager.Time.text = "Время: " + endTime.Substring(0, endTime.IndexOf(','));
        }
        

    }

}
