using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerScript : MonoBehaviour {

    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    private Vector2 inputValue;
    private Animator myAnimator;
    private bool isGrounded = false;

    private SpriteRenderer spriteRenderer;
    private List<Sprite> spritesList;
    private Dictionary<int, PolygonCollider2D> spriteColliders;
    private bool _processing;
    private int _frame;
    public int Frame
    {
        get { return _frame; }
        set
        {
            if (value != _frame)
            {
                if (value > -1)
                {
                    spriteColliders[_frame].enabled = false;
                    if (spriteColliders[_frame].IsTouchingLayers(LayerMask.GetMask("Ground")))
                    {
                        isGrounded = true;
                        Debug.Log(isGrounded);
                    }
                    _frame = value;
                    spriteColliders[_frame].enabled = true;
                    if (spriteColliders[_frame].IsTouchingLayers(LayerMask.GetMask("Ground")))
                    {
                        isGrounded = true;
                        Debug.Log(isGrounded);
                    }
                }
                else
                {
                    _processing = true;
                    StartCoroutine(AddSpriteCollider(spriteRenderer.sprite));
                }
            }
        }
    }

    private void Start()
    {
        transform.localScale = new Vector2(Mathf.Sign(-transform.position.x), 1f);
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_processing)
        {
            foreach(PolygonCollider2D col in spriteColliders.Values)
            {
                if (col.IsTouchingLayers(LayerMask.GetMask("Ground")) && col.enabled == true)
                {
                    isGrounded = true;
                }
            }
        }

        this.Run();
        this.FlipSprite();
        if (isGrounded)
        {
            this.Jump();
            this.Duck();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(inputValue * Time.deltaTime);
    }

    void Run()
    {
        inputValue.x = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed;
        inputValue.y = CrossPlatformInputManager.GetAxis("Vertical");

        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            myAnimator.SetBool("Running", true);
        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") == 0)
        {
            myAnimator.SetBool("Running", false);
        }
        
        if (isGrounded)
        {
            myAnimator.SetBool("Jumping", false);
        }
    }

    private void Jump()
    {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                myAnimator.SetBool("Jumping", true);
                inputValue.y = jumpSpeed;
                isGrounded = false;
            }
            else if (CrossPlatformInputManager.GetButtonDown("Vertical") && CrossPlatformInputManager.GetAxis("Vertical") > 0)
            {
                myAnimator.SetBool("Jumping", true);
                inputValue.y = jumpSpeed;
                isGrounded = false;
            }
    }

    private void Duck()
    {
        if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
        {
            myAnimator.SetBool("Ducking", true);
            inputValue.x = 0f;
        }
        if (CrossPlatformInputManager.GetAxis("Vertical") == 0)
        {
            myAnimator.SetBool("Ducking", false);
        }
    }

    private void FlipSprite()
    {
        if(CrossPlatformInputManager.GetAxis("Horizontal") != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(inputValue.x), 1f);
        }
    }

    private IEnumerator AddSpriteCollider(Sprite sprite)
    {
        spritesList.Add(sprite);
        int index = spritesList.IndexOf(sprite);
        PolygonCollider2D spriteCollider = gameObject.AddComponent<PolygonCollider2D>();
        //    spriteCollider.sharedMaterial = _material;
        spriteColliders.Add(index, spriteCollider);
        yield return new WaitForEndOfFrame();
        Frame = index;
        _processing = false;
    }

    private void OnEnable()
    {
        spriteColliders[Frame].enabled = true;
    }

    private void OnDisable()
    {
        spriteColliders[Frame].enabled = false;
    }

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        spritesList = new List<Sprite>();

        spriteColliders = new Dictionary<int, PolygonCollider2D>();

        Frame = spritesList.IndexOf(spriteRenderer.sprite);
    }

    private void LateUpdate()
    {
        if (!_processing)
            Frame = spritesList.IndexOf(spriteRenderer.sprite);
    }

}
