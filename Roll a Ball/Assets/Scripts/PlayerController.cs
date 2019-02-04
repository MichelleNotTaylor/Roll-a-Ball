using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public Text countText;
	public Text winText;
	public float jumpForce = 1.0f;
	public Vector3 jump;
	public bool isGrounded;
	public Vector3 spawnPoint;
	public float deathLine = -10f;

	private Rigidbody rb;
	private int count;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
		spawnPoint = rb.transform.position;
		count = 0;
		SetCountText();
		winText.text = "";
		jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

	void OnCollisionStay()
	{
		isGrounded = true;
	}

	void FixedUpdate()
    {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(jump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}

		if(rb.transform.position.y <= deathLine)
		{
			rb.transform.position = spawnPoint;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();
		if (count >= 8)
		{
			winText.text = "You Win!";
		}
	}

}
