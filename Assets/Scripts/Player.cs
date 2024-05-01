using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Player : MonoBehaviour
{
    public bool IsGrounded = false;
    public float MoveSpeed;
    public float JumpForce;
    public Rigidbody rb;
    public Animator anim;


    public Transform groundCheckPoint;

    public float groundCheckRadius;
    public LayerMask groundLayer;
    public LayerMask MovingPlatLAYER;
    public bool isTouchingGround;

    public static bool PlayerOnTheMovingPlat = false;
    public int JumpCount; //0 normal  //1takla



    public int isPlayerDEAD = 0;//0 alive  1 dead  2untouchable


    public float jumpFrequency = .002f;
    public float nextJumpTime;



    public float FlyAmounth = 0f;
    public bool FlyTime = false;
    public GameObject AðýzdakiSakýz;



    private bool isWallSliding;
    private float wallSlidingSpeed = 200f;

    private bool isWallJumping;
    private float wallJumpingDirection= 1;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isFacingRight = true;

    public GameObject[] StickyGHumss;

    public bool StickTime = false;
    public bool ShieldTime = false;

    public GameObject ShieldObject;

    public GameObject Ybalon;
    public GameObject Mbalon;

    public GameObject CrafttingPanel;


    public bool BalonAktif;



    public Transform[] CheckPointTransforms;



    public int Acount;
    public int Bcount;
    public int Ccount;
    public int Dcount;
    public int Ecount;
    public int Fcount;


    public TMP_Text AcountTXT;
    public TMP_Text BcountTXT;
    public TMP_Text CcountTXT;
    public TMP_Text DcountTXT;
    public TMP_Text EcountTXT;
    public TMP_Text FcountTXT;

    public TMP_Text UcancountTXT;
    public TMP_Text KalkancountTXT;
    public TMP_Text YapýskancountTXT;
    public TMP_Text OldurencountTXT;


    public Button UcanSbtn;
    public Button KalkanSbtn;
    public Button YapýskanSbtn;
    public Button OldurenSbtn;

    public int Ucancount;
    public int Kalkancount;
    public int Yapýskancount;
    public int Oldurencount;

    public GameObject PauseScreen;
    public GameObject EndObject;
    public GameObject MüzikÇalar;


    public GameObject YardýmPanel;
    public void AdetTUTUCU()
    {
        AcountTXT.text = Acount.ToString();
        BcountTXT.text = Bcount.ToString();
        CcountTXT.text = Ccount.ToString();
        DcountTXT.text = Dcount.ToString();
        EcountTXT.text = Ecount.ToString();
        FcountTXT.text = Fcount.ToString();

        UcancountTXT.text = Ucancount.ToString();
        KalkancountTXT.text = Kalkancount.ToString();
        YapýskancountTXT.text = Yapýskancount.ToString();
        OldurencountTXT.text = Oldurencount.ToString();
    }
    void Start()
    {

        transform.position = CheckPointTransforms[PlayerPrefs.GetInt("CheckPoint")].position;

        SoundManager.PlaySound("SpawnSesi");
        if (PlayerPrefs.GetInt("YardýmEdildimi") == 0)
        {
            YardýmPanel.SetActive(true);
            PlayerPrefs.SetInt("YardýmEdildimi", 1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isWallSliding && isPlayerDEAD ==0)
        {
            var move1 = new Vector3(Input.GetAxis("Horizontal"), 0); //buttonlar için

            transform.position += move1 * MoveSpeed * Time.deltaTime;
        }
    }

    void Update()
    {
        ButonKontrol();
        AdetTUTUCU();
        //  IsWalled();

        if (!FlyTime && StickTime)
        {
            WallSlide();
            WallJump();

        }
       


        isTouchingGround = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isTouchingGround)
        {
            JumpCount = 0;
        }

        if (Input.GetAxis("Horizontal") < 0f  && isPlayerDEAD == 0)
        {
            var rotationVector = new Vector2(0, 270);
            transform.rotation = Quaternion.Euler(rotationVector);
        }
        if (Input.GetAxis("Horizontal") > 0f  && isPlayerDEAD == 0)
        {
            var rotationVector = new Vector2(0, 90);
            transform.rotation = Quaternion.Euler(rotationVector);
        }
    

        PlayerOnTheMovingPlat = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, MovingPlatLAYER);


        if (PlayerOnTheMovingPlat) //moving plat ground layer olmadýðýndan 
        {
            isTouchingGround = true;
        }



        //if (Input.GetButtonDown("Jump") && JumpCount ==0  && isPlayerDEAD != 1 && transform.gameObject.GetComponent<Rigidbody>().velocity.y == 0)
        //{

        //    JumpCount++;
        //    rb.velocity = new Vector2(0, 0);
        //    rb.AddForce(new Vector2(0, JumpForce), ForceMode.Impulse);



        //},,
        if (Input.GetButtonDown("Jump") && isTouchingGround && (nextJumpTime < Time.timeSinceLevelLoad) && isPlayerDEAD == 0)
        {
            nextJumpTime = Time.timeSinceLevelLoad + jumpFrequency;
            Jump();
        }
        void Jump()
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, JumpForce), ForceMode.Impulse);
        }


        if ((Input.GetAxis("Horizontal") != 0 && anim.GetBool("isPushing") == false
    && rb.velocity.y < 0.01f && rb.velocity.y > -0.01f) && isPlayerDEAD == 0 && FlyTime == false )
        {
            anim.SetBool("isRunning", true);

        }
        else
        {
            anim.SetBool("isRunning", false);

        }


        if (rb.velocity.y < 0.01f && rb.velocity.y > -0.01f)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isfall", false);

        }
        if (rb.velocity.y > 0.01f )
        {

                anim.SetBool("isJump", true);

            
            anim.SetBool("isfall", false);

        }
        else if (rb.velocity.y < -0.01f)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isfall", true);

        }
       
        if (FlyTime)
        {
            var moveUP = new Vector3(0, 1); //buttonlar için
            transform.position += moveUP * FlyAmounth * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.H) && !BalonAktif &&Ucancount>0 && isPlayerDEAD == 0)
        {
            Ucancount--;
            StartCoroutine(FlyTimeMetot());
        }
        if (Input.GetKeyDown(KeyCode.J) && !BalonAktif && Kalkancount > 0 && isPlayerDEAD == 0)
        {
            Kalkancount--;
            StartCoroutine(ShieldTimeMetot());
        }
        if (Input.GetKeyDown(KeyCode.K) && !BalonAktif && Yapýskancount > 0 && isPlayerDEAD == 0)
        {
            Yapýskancount--;
            StartCoroutine(StickTimeMetot());
       
        }
        if (Input.GetKeyDown(KeyCode.L) && !BalonAktif && Oldurencount > 0 && isPlayerDEAD == 0) //öldüren
        {
            Oldurencount--;
            StartCoroutine(DeadTimeMetot());
        }

        if (Input.GetKeyDown(KeyCode.E) && isPlayerDEAD == 0) //crafting
        {
            CraftinPAçkapa(true);
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isPlayerDEAD == 0) //crafting
        {
            if (PauseScreen.activeInHierarchy)
            {
                PauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                PauseScreen.SetActive(true);
                Time.timeScale = 0;
            }
          
        }

    }
    public void ESCPAçkapa(bool onOFF)
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(onOFF);
    }
    public void MenuBTN()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }

    public void CraftinPAçkapa(bool onOFF)
    {
        CrafttingPanel.SetActive(onOFF);
    }

    IEnumerator FlyTimeMetot()
    {
        BalonAktif = true;
        SoundManager.PlaySound("BalonSisirme");
        rb.velocity = new Vector2(0, 0);
        FlyTime = true;
        AðýzdakiSakýz.SetActive(true);
        anim.SetInteger("isFLY", 1);
        Physics.gravity = new Vector3(0, -0f, 0); 
        yield return new WaitForSeconds(1f);

        anim.SetInteger("isFLY", 2);
        yield return new WaitForSeconds(3.8f);

        SoundManager.PlaySound("BalonPatlat");
        yield return new WaitForSeconds(0.2f);


        AðýzdakiSakýz.SetActive(false);
        Physics.gravity = new Vector3(0, -20F, 0); //normal gravity
        anim.SetInteger("isFLY", 0);
        FlyTime = false;

        BalonAktif = false;
    }
    IEnumerator StickTimeMetot()
    {
        BalonAktif = true;
        Ybalon.SetActive(true);
        Ybalon.GetComponent<Animator>().SetBool("isYbalon", true);

        for (int i = 0; i < 4; i++)
        {
            SoundManager.PlaySound("BalonSisirme");
            yield return new WaitForSeconds(0.2775f);
            SoundManager.PlaySound("SakizPatlama");
            StickyGHumss[i].SetActive(true);

            //patlama sesi
        }

        Ybalon.GetComponent<Animator>().SetBool("isYbalon", false);

        Ybalon.SetActive(false);
        StickTime = true;
        //for (int i = 0; i < StickyGHumss.Length; i++)
        //{
        //    StickyGHumss[i].SetActive(true);
        //}

        yield return new WaitForSeconds(5.8f);

        SoundManager.PlaySound("BalonPatlat");
        yield return new WaitForSeconds(0.2f);


        for (int i = 0; i < StickyGHumss.Length; i++)
        {
            StickyGHumss[i].SetActive(false);
        }

        isWallSliding = false;
        Physics.gravity = new Vector3(0, -20f, 0);
        anim.SetBool("isStick", false);

        StickTime = false;
        BalonAktif = false;
    }

    IEnumerator ShieldTimeMetot()
    {
        BalonAktif = true;
        SoundManager.PlaySound("BalonSisirme");
        

        Mbalon.SetActive(true);
        Mbalon.GetComponent<Animator>().SetBool("isMbalon", true);
        yield return new WaitForSeconds(0.9f);
        SoundManager.PlaySound("BalonPatlat");
        yield return new WaitForSeconds(0.2f);


        Mbalon.SetActive(false);
        Mbalon.GetComponent<Animator>().SetBool("isMbalon", false);
        ShieldTime = true;
        ShieldObject.SetActive(true);
     

        yield return new WaitForSeconds(4.8f);

        SoundManager.PlaySound("BalonPatlat");
        yield return new WaitForSeconds(0.2f);

        ShieldObject.SetActive(false);


        ShieldTime = false;

        BalonAktif = false;
    }
    IEnumerator DeadTimeMetot()
    {
        BalonAktif = true;
        SoundManager.PlaySound("SakizCigneme");


       
        yield return new WaitForSeconds(2f);

        if (isPlayerDEAD == 0)
        {
            StartCoroutine(DeadAnim());
        }

       

        BalonAktif = false;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Spikes" && !ShieldTime)  //player moving plata deðdiðinde
        {
            Debug.Log("Balon deðdi");


            if (isPlayerDEAD == 0)
            {
                StartCoroutine(DeadAnim());
            }
        }

    }
    private void OnTriggerEnter(Collider collision)
    {

        if (PlayerOnTheMovingPlat && collision.gameObject.tag == "MovingPlatform")  //player moving plata deðdiðinde
        {
            //   Debug.Log("Player deðdi");
            transform.SetParent(collision.transform);
        }

        if (collision.gameObject.tag == "Spikes" && !ShieldTime)  //player moving plata deðdiðinde
        {
             Debug.Log("Balon deðdi");


           if(isPlayerDEAD == 0)
            {
                StartCoroutine(DeadAnim());
            }
        }




        if (collision.gameObject.tag == "CH1" && PlayerPrefs.GetInt("CheckPoint") < 1) 
        {
            PlayerPrefs.SetInt("CheckPoint", 1);

            collision.gameObject.GetComponent<Bayrak>().BayrakAç();

            SoundManager.PlaySound("SesEfekti1");
        }


        if (collision.gameObject.tag == "CH2" && PlayerPrefs.GetInt("CheckPoint") < 2)
        {
            PlayerPrefs.SetInt("CheckPoint", 2);

            collision.gameObject.GetComponent<Bayrak>().BayrakAç();

            SoundManager.PlaySound("SesEfekti1");
        }


        if (collision.gameObject.tag == "CH3" && PlayerPrefs.GetInt("CheckPoint") < 3)
        {
            PlayerPrefs.SetInt("CheckPoint", 3); SoundManager.PlaySound("SesEfekti1");

            collision.gameObject.GetComponent<Bayrak>().BayrakAç();
        }
        if (collision.gameObject.tag == "CH4" && PlayerPrefs.GetInt("CheckPoint") < 4)
        {
            PlayerPrefs.SetInt("CheckPoint", 4); SoundManager.PlaySound("SesEfekti1");

            collision.gameObject.GetComponent<Bayrak>().BayrakAç();
        }
        if (collision.gameObject.tag == "CH5" && PlayerPrefs.GetInt("CheckPoint") < 5)
        {
            PlayerPrefs.SetInt("CheckPoint", 5); SoundManager.PlaySound("SesEfekti1");

            collision.gameObject.GetComponent<Bayrak>().BayrakAç();
        }
        if (collision.gameObject.tag == "CH6" && PlayerPrefs.GetInt("CheckPoint") < 6)
        {
            PlayerPrefs.SetInt("CheckPoint", 6); SoundManager.PlaySound("SesEfekti1");

            collision.gameObject.GetComponent<Bayrak>().BayrakAç();
        }


        if (collision.gameObject.tag == "A")
        {
            Destroy(collision.gameObject); SoundManager.PlaySound("PickUpSoundEffect");

            Acount++;
        }

        if (collision.gameObject.tag == "B")
        {
            Bcount++;
            Destroy(collision.gameObject); SoundManager.PlaySound("PickUpSoundEffect");
        }
        if (collision.gameObject.tag == "C")
        {
            Ccount++;
            Destroy(collision.gameObject); SoundManager.PlaySound("PickUpSoundEffect");
        }
        if (collision.gameObject.tag == "D")
        {
            Dcount++;
            Destroy(collision.gameObject); SoundManager.PlaySound("PickUpSoundEffect");
        }
        if (collision.gameObject.tag == "E")
        {
            Ecount++;
            Destroy(collision.gameObject); SoundManager.PlaySound("PickUpSoundEffect");
        }

        if (collision.gameObject.tag == "F")
        {
            Fcount++;
            Destroy(collision.gameObject); SoundManager.PlaySound("PickUpSoundEffect");
        }


        if (collision.gameObject.tag == "END")  //player moving plata deðdiðinde
        {
          
        
            if (isPlayerDEAD == 0)
            {
              //  isPlayerDEAD = 2;
                StartCoroutine(EndAnim());
            }
        }

    }
    public void ButonKontrol()
    {

        if(Acount > 0 && Bcount > 0 && Ccount > 0)//UÇAN
        {
            UcanSbtn.interactable = true;
        }
        else
        {
            UcanSbtn.interactable = false;
        }
        if (Dcount > 0 && Bcount > 0 && Ccount > 0)//KALKAN
        {
            KalkanSbtn.interactable = true;
        }
        else
        {
            KalkanSbtn.interactable = false;
        }
        if (Dcount > 0 && Ecount > 0 && Ccount > 0)//YAPIÞKAN
        {
            YapýskanSbtn.interactable = true;
        }
        else
        {
            YapýskanSbtn.interactable = false;
        }
        if (Fcount > 0 && Ccount > 0)//ÖLDÜREN
        {
            OldurenSbtn.interactable = true;
        }
        else
        {
            OldurenSbtn.interactable = false;

        }
    }
    public void UcanSbtnMetot()
    {
        SoundManager.PlaySound("HasarAlma");
        
        Acount--;
        Bcount--;
        Ccount--;
        Ucancount++;
    }
    public void KalkanSbtnMetot()
    {
        SoundManager.PlaySound("HasarAlma");
        Dcount--;
        Bcount--;
        Ccount--;
        Kalkancount++;
    }
    public void YapýskanSbtnMetot()
    {
        SoundManager.PlaySound("HasarAlma");
        Dcount--;
        Ecount--;
        Ccount--;
        Yapýskancount++;
    }
    public void OldurenSbtnMetot()
    {
        SoundManager.PlaySound("HasarAlma");
        Fcount--;
        Ccount--;
        Oldurencount++;
    }


    private bool IsWalled()
    {
        return Physics.CheckSphere(wallCheck.position, 0.2f, wallLayer);
    }
    private void WallSlide()
    {
        if (IsWalled() && !isTouchingGround )
        {
            isWallSliding = true;
            //   rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

            Physics.gravity = new Vector3(0, -0f, 0);
            rb.velocity = new Vector2(0, 0);

            anim.SetBool("isStick", true);
        }
        else
        {
            isWallSliding = false;
            Physics.gravity = new Vector3(0, -20f, 0);

            anim.SetBool("isStick", false);
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
         //   wallJumpingDirection = -transform.localScale.z;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            transform.Rotate(0, 180, 0);
            anim.SetBool("isStick", false);
            rb.velocity = new Vector2(0, 0);

            Physics.gravity = new Vector3(0, -20F, 0); //normal gravity

            isWallJumping = true;

 
            rb.velocity = new Vector2((transform.rotation.y / -11) * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;


            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    IEnumerator DeadAnim()
    {
        isPlayerDEAD = 1;
        anim.SetBool("isDead", true);
        SoundManager.PlaySound("OlmeSesiDaha");
        yield return new WaitForSeconds(3f);



        SceneManager.LoadScene("game");
    }


    IEnumerator EndAnim()
    {
        MüzikÇalar.SetActive(false);
           isPlayerDEAD = 2;
        anim.SetBool("isEND", true);
        SoundManager.PlaySound("END1");
        yield return new WaitForSeconds(4f);

        SoundManager.PlaySound("END2");
        yield return new WaitForSeconds(1f);
        SoundManager.PlaySound("END3");
        EndObject.SetActive(true);

    }   


    public void bolum_ac(string bolum_ismi)
    {
        SceneManager.LoadScene(bolum_ismi);
    }
    public void YardýmPanelBTN(bool ONoff)
    {
        YardýmPanel.SetActive(ONoff);
    }
}
