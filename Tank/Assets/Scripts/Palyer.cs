using UnityEngine;

public class Palyer : MonoBehaviour
{
    public float speed;
    public Sprite[] tankSprits; // up down right left
    public GameObject bullet;
    public GameObject boom;
    public GameObject shield;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    private SpriteRenderer sr; 
    private Vector3 bulletAng;
    private float timeBullet = 1;
    private bool isDefend;
    private float defendTime;



    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
        sr = GetComponent<SpriteRenderer>();
        bulletAng = new Vector3(0, 0, 0);

        isDefend = true;
        defendTime = 3;
        shield.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDefend)
        {
            //Debug.Log("11111   " + defendTime);
            //shield.SetActive(true);
            defendTime -= Time.deltaTime;
            //Debug.Log("22222   " + defendTime);
            if (defendTime <= 0)
            {
                Debug.Log("is not defend");
                isDefend = false;
                // Destroy(shield.gameObject);
                shield.SetActive(false);

            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayManager.Instance.isDefeat)
        {
            return;
        }
        tankMove();

        if (timeBullet > 0.4f)
        {
            tankAttack();
        }
        else
        {
            timeBullet += Time.fixedDeltaTime;
        }
    }

    public void tankMove()
    {
        // 水平
        float h = Input.GetAxisRaw("Horizontal");
        //Debug.Log("h: " + h);
        transform.Translate(Vector3.right * h * Time.fixedDeltaTime * speed, Space.World);
        if (h > 0)
        {
            // right
            sr.sprite = tankSprits[2];
            bulletAng.z = -90;
        }
        else if (h < 0)
        {
            // left
            sr.sprite = tankSprits[3];
            bulletAng.z = 90;
        }

        if (Mathf.Abs(h) >0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (h != 0)
        {
            return;
        }

        // 垂直
        float v = Input.GetAxisRaw("Vertical");
        //Debug.Log("h: " + v);
        transform.Translate(Vector3.up * v * Time.fixedDeltaTime * speed, Space.World);
        if (v > 0)
        {
            // up
            sr.sprite = tankSprits[0];
            bulletAng.z = 0;
        }
        else if (v < 0)
        {
            // down
            sr.sprite = tankSprits[1];
            bulletAng.z = 180;
        }

        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

    }

    private void tankAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles + bulletAng));
            obj.GetComponent<Bullet>().isFromPlay = true;
            timeBullet = 0;
        }
    }

    private void tankDie()
    {
        if (isDefend)
            return;

        PlayManager.Instance.isDead = true;
        Instantiate(boom, transform.position, transform.rotation);
        Destroy(gameObject);

        
    }
}
