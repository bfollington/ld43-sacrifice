using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Pixelplacement;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IHurtable
{
    #region Private Members

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private int startHealth;

    private bool _isRolling = false;
    private bool _isAttacking = false;
    private bool _disableAnimation = false;

    #endregion

    #region Public Members

    public float Speed = 5.0f;

    public float RotationSpeed = 240.0f;

    public float JumpSpeed = 7.0f;

    public Animator Animator;
    public SpriteRenderer SpriteRenderer;
    public GameObject AttackHurtbox;
    public Checklist Checklist;

    public AudioSource JumpSound;
    public AudioSource RollSound;
    public AudioSource HurtSound;
    public AudioSource StabSound;

    #endregion

    // Use this for initialization
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
//        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("TriggerOnly"));
    }

    #region Health & Hunger

    [Tooltip("Amount of health")]
    public int Health = 100;

    public bool IsDead
    {
        get
        {
            return Health == 0;
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;

        if (IsDead)
        {
            
        }

    }

    #endregion


    void FixedUpdate()
    {
        
    }

    private bool mIsControlEnabled = true;
    private bool _isDead;

    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }
    
    public void EnableAnimation()
    {
        _disableAnimation = false;
    }

    public void DisableAnimation()
    {
        _disableAnimation = true;
    }

    public void GetShield()
    {
        DisableAnimation();
        Animator.Play("got_shield");
    }
    
    public void GetSword()
    {
        DisableAnimation();
        Animator.Play("got_sword");
    }

    public void Sacrifice()
    {
        _isDead = true;
        Animator.Play("sacrificed");
        Animator.speed = 1;
    } 

    // Update is called once per frame
    void Update()
    {
        if (!IsDead && mIsControlEnabled)
        {
            // Get Input for axis
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (h < -0.05f)
            {
                SpriteRenderer.flipX = true;
            } else if (h > 0.05f)
            {
                SpriteRenderer.flipX = false;
            }
            
            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);
           
            if (_characterController.isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;
                _moveDirection *= Speed;

                if (Input.GetButton("Jump"))
                {
                    _moveDirection.y = JumpSpeed;
                    JumpSound.Play();
                }
                
                if (!_isRolling && !_isAttacking)
                {
                    if (Input.GetButton("Roll"))
                    {
                        RollSound.Play();
                        StartCoroutine(Roll(move));
                    }
                
                    if (Checklist.HasSword && Input.GetButton("Attack"))
                    {
                        StabSound.Play();
                        StartCoroutine(Attack());
                    }
                }
            } 
        }

        _moveDirection.y -= Gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);

        if (!_isDead && !_isRolling && !_isAttacking && !_disableAnimation)
        {
            if (_characterController.isGrounded)
            {
                if (_characterController.velocity.magnitude > 0)
                {
                    Animator.Play("run");
                    Animator.speed = _characterController.velocity.magnitude / 2f;
                }
                else
                {
                    Animator.Play("idle");
                }
            }
            else
            {
                Animator.Play("jump");
                Animator.speed = 1;
            }
        }
    }

    public void Stop()
    {
        _moveDirection = Vector3.zero;
    }
    
    public IEnumerator Roll(Vector3 move)
    {
        float turnAmount = Mathf.Atan2(move.x, move.z);
        transform.Rotate(0, turnAmount, 0);
        _moveDirection = transform.forward * 7f;
        
        Animator.Play("roll");
        Animator.speed = 2;
        _isRolling = true;
        DisableControl();

        yield return new WaitForSeconds(0.35f);
        
        EnableControl();
        Animator.Play("idle");
        _isRolling = false;
    }
    
    public IEnumerator Attack()
    {
        _moveDirection = Vector3.zero;
        Animator.Play("attack");
        Animator.speed = 2;
        _isAttacking = true;
        DisableControl();

        yield return new WaitForSeconds(0.2f);

        var hurtbox = Instantiate(AttackHurtbox);
        hurtbox.transform.position = transform.position + transform.forward + new Vector3(0, 0, 0.4f);
        
        yield return new WaitForSeconds(0.2f);
        
        EnableControl();
        Animator.Play("idle");
        _isAttacking = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
//        InteractableItemBase item = other.GetComponent<InteractableItemBase>();
//
//        if (item != null)
//        {
//            if (item.CanInteract(other))
//            {
//
//                mInteractItem = item;
//
//                Hud.OpenMessagePanel(mInteractItem);
//            }
//        }
    }

    private void OnTriggerExit(Collider other)
    {
//        InteractableItemBase item = other.GetComponent<InteractableItemBase>();
//        if (item != null)
//        {
//            Hud.CloseMessagePanel();
//            mInteractItem = null;
//        }
    }

    public void Hurt(float damage)
    {
        HurtSound.Play();
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Tween.LocalScale(transform, new Vector3(1, 1, 1), 0.5f, 0, Tween.EaseBounce);
    }
}
