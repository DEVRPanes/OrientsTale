using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	[Header("Components")]
	public Joystick joystick;
	public Rigidbody2D rigidBody2D;
	public Toggle hoodT;

	public Animator animator;

	[SerializeField]
	[Header("Player Stats")]
	public int maxHealth;
	public int currentHealth;
	public int healthRegen = 1;
	public float timeValue = 2.5f;

	[Header("Game Objects")]
	public GameObject questButton;
	public GameObject talkButton;
	public GameObject interactButton;
	public GameObject followToggle;
	public GameObject heart1;
	public GameObject heart2;
	public GameObject heart3;
	public GameObject heart4;
	public GameObject heart5;
	public GameObject GameOver;
	public GameObject HealthRegen;

	[SerializeField]
	[Header("Movement Statistics")]
	public float msHooded;
	public float msUnhooded;
	private float movementSpeed;

	[SerializeField]
	[Header("States")]
	public bool isDead = false;
	public bool facingRight = true;
	public bool canFlip = true;
	public bool isHooded = true;
	public bool canHood = true;

	[SerializeField]
	[Header("Get States")]
	public int hoodMode = 0;
	public int facingDirection = 0;

	[Header("Interaction")]
	public float interactRadius;

	[SerializeField]
	private Transform interactHitBox;

	[Header("Pickable")]
	public float pickableRadius;

	[SerializeField]
	private Transform pickableHitBox;

	[SerializeField]
	[Header("Debug")]
	public bool debugMode;

	float horizontalMove = 0f;
	float verticalMove = 0f;

	[Header("Quests")]
	[SerializeField]
	public QuestEvent quest;

	[Header("LayerMasks")]
	[SerializeField]
	private LayerMask whatIsIntereactable;

	[SerializeField]
	private LayerMask whatIsPickable;

	[SerializeField]
	private LayerMask whatIsDamageable;

	[SerializeField] private FlashEffect simpleFlashEffect;

	void Start()
	{

	}

	void Update()
    {

		// Check Debug Mode
		CheckDebugMode();

		if (isDead != true)
        {
			// Movement
			PlayerHealthRegen();
			CheckMovementInput();
		}
		else
        {
			horizontalMove = 0f;
			verticalMove = 0f;
			animator.SetFloat("Speed", 0);
			HealthRegen.SetActive(false);

		}
	}

	private void CheckDebugMode() 
	{
		//Debug Toggle
		if (debugMode == true)
		{
			if (this.transform.hasChanged)
			{
				Debug.Log("Joystick Horizontal = " + joystick.Horizontal);
				Debug.Log("Joystick Vertical = " + joystick.Vertical);
				Debug.Log("Joystick Direction = " + joystick.Direction);
				Debug.Log("Player Horizontal Speed = " + horizontalMove);
				Debug.Log("Player Vertical Speed = " + verticalMove);
			}
			transform.hasChanged = false;
		}
	}

	private void CheckMovementInput() 
	{
		if (isHooded == true) 
		{
			// Speed value
			movementSpeed = msHooded;
		}
        else
        {
			// Speed value
			movementSpeed = msUnhooded;
		}

		horizontalMove = joystick.Horizontal * movementSpeed;
		verticalMove = joystick.Vertical * movementSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove + verticalMove));

		// Check Player states facing
		if (canFlip == true)
		{
			if (horizontalMove > 0 && !facingRight)
			{
				// Flip player right
				Flip();
			}
			else if (horizontalMove < 0 && facingRight)
			{
				// Flip player left
				Flip();
			}
		}
	}

	public void DisableFlip()
	{
		canFlip = false;
	}

	public void EnableFlip()
	{
		canFlip = true;
	}

	private void Flip()
	{
		// Switch the way the player labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public int GetFacingDirection()
	{
		return facingDirection;
	}

	public void HoodToggle()
	{
		if (canHood == true) 
		{ 
			if (isHooded != true)
			{
				isHooded = true;
				animator.SetBool("isHooded", true);
				hoodMode = 1;
			}
			else
			{
				isHooded = false;
				animator.SetBool("isHooded", false);
				hoodMode = 0;
			}
		}
	}

	public int GetCurrentHoodMode()
	{
		return hoodMode;
	}

	private void Damage(int amount)
	{
		currentHealth -= amount;

		//Instantiate(hitParticle, aliveAnim.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
		simpleFlashEffect.Flash();
		animator.SetTrigger("damage");

		if (currentHealth <= 0.0f)
		{
			//aliveGO.SetActive(false);
			isDead = true;
			Debug.Log("Dead");
			GameOver.SetActive(true);
		}

		PlayerHealth();

	}

	private void PlayerHealth()
    {
		if (currentHealth == 8)
        {
			heart5.SetActive(false);
		}
		else if (currentHealth == 6)
		{
			heart4.SetActive(false);
		}
		else if (currentHealth == 4)
		{
			heart3.SetActive(false);
		}
		else if (currentHealth == 2)
		{
			heart2.SetActive(false);
		}
		else if (currentHealth == 0)
		{
			heart1.SetActive(false);
		}
	}

	private void PlayerHealthRegen()
    {
		if (isHooded && currentHealth != 10)
		{
			if (timeValue >= 0)
			{
				timeValue -= Time.deltaTime;
			}
			else
            {
				timeValue = 2.5f;
				currentHealth += 1;
            }
			HealthRegen.SetActive(true);
		}
		else
        {
			HealthRegen.SetActive(false);
		}
		PlayerHealthRegenUI();
	}

	private void PlayerHealthRegenUI()
    {
		if (currentHealth == 10)
		{
			heart5.SetActive(true);
		}
		else if (currentHealth == 8)
		{
			heart4.SetActive(true);
		}
		else if (currentHealth == 6)
		{
			heart3.SetActive(true);
		}
		else if (currentHealth == 4)
		{
			heart2.SetActive(true);
		}
		else if (currentHealth == 2)
		{
			heart1.SetActive(true);
		}
	}

	void FixedUpdate()
	{
		// Move Player
		transform.position += new Vector3 (horizontalMove * movementSpeed * Time.fixedDeltaTime, verticalMove * movementSpeed * Time.fixedDeltaTime, 0);
	}

	public void DialogueCollision()
    {
		if (isHooded == true)
        {
			talkButton.SetActive(true);
        }
		else if (isHooded == false)
        {
			talkButton.SetActive(false);
		}
    }

	public void DialogueTrigger()
    {
		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(interactHitBox.position, interactRadius, whatIsIntereactable);

		foreach (Collider2D collider in detectedObjects)
		{
			collider.gameObject.SendMessage("StartDialogue");
		}
	}

	public void QuestCollision()
	{
		if (isHooded == true)
		{
			questButton.SetActive(true);
		}
		else if (isHooded == false)
		{
			questButton.SetActive(false);
		}
	}

	public void QuestTrigger()
    {
		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(interactHitBox.position, interactRadius, whatIsIntereactable);

		foreach (Collider2D collider in detectedObjects)
		{
			collider.gameObject.SendMessage("StartDialogue");
		}

		quest.startQuestEvent = true;

	}

	public void IntereactableCollision()
	{
		if (isHooded == true)
		{
			interactButton.SetActive(true);
		}
		else if (isHooded == false)
		{
			interactButton.SetActive(false);
		}
	}

	public void IntereactableTrigger()
	{
		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(interactHitBox.position, interactRadius, whatIsIntereactable);

		foreach (Collider2D collider in detectedObjects)
		{
			collider.gameObject.SendMessage("PickupItem");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(interactHitBox.position, interactRadius);
		Gizmos.DrawWireSphere(pickableHitBox.position, pickableRadius);
	}
}
