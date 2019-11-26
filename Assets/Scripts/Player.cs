using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UI;

public class Player : MonoBehaviour {

// inisialisasi gerakan
	private CharacterController karakter;
	private Animator animator;
	public float forwardmove = 6f;
	public float backwardmove = 4;
	public float turnSpeed = 100f;

	// inisialisasi serangan
	public GameObject peluru;
	public float kecPeluru = 1500f;
	public Transform munculPeluru;
	public float delayTembak;
	private float attackTimer;
	public ParticleSystem muzzle;
	public AudioSource tembakan;
	public int ammo = 20;
	public Transform NPC;
	public int health = 250;
	// public Slider healthBar;
	public float jarak;
	// Use this for initialization
	void OnGUI(){
		string Jarak = "Jarak : " + jarak;
		GUI.Box( new Rect(700, 560, 120, 25), Jarak);

		string Ammo = "Ammo : " + ammo;
		GUI.Box( new Rect(580, 560, 100, 25), Ammo);

		string Health = "Health : " + health;
		GUI.Box( new Rect(580, 520, 100, 25), Health);
	}
	void Awake(){
		karakter = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}
	void Start () {
		tembakan = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		jarak = Vector3.Distance(transform.position, NPC.transform.position);

		if(Input.GetKeyDown(KeyCode.Z)){
			Tembak();
			tembakan.Play();
			if(ammo > 0){
				ammo--;
			}
			// muzzle.Play();
		}
		if(Input.GetKeyDown(KeyCode.X)){
			animator.SetBool("IsPisau", true);
			Pisau();
		}else{
			animator.SetBool("IsPisau", false);
		}
		if(Input.GetKeyDown(KeyCode.C)){
			animator.SetBool("IsGranat", true);
			Granat();
		}else{
			animator.SetBool("IsGranat", false);
		}


		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");

		var movement = new Vector3(horizontal, 0, vertical);

		animator.SetFloat("Speed", vertical);

		transform.Rotate(Vector3.up, horizontal * Time.deltaTime * turnSpeed);
		if(vertical != 0)
		{
			float moveSpeed = (vertical > 0 )?forwardmove : backwardmove;
			karakter.SimpleMove(transform.forward * moveSpeed * vertical);
		}
	}
	void Tembak(){
		// attackTimer -= Time.deltaTime;
		
		muzzle.Play();
		tembakan.Play();
		Ray ray = Camera.main.ViewportPointToRay(Vector3.one*0.5f);
		// RaycastHit info;
		// Debug.DrawRay(ray.origin, ray.direction*100, Color.red, 2f);
		// Ray ray = Camera.main
		// RaycastHit info;
		// if(Physics.Raycast(ray, out info, 100)){
			// print("kena musuh");
			attackTimer -= Time.deltaTime;

			if(attackTimer <= 0)
			{
				GameObject peluruPlayer = Instantiate(peluru, munculPeluru.transform.position, munculPeluru.rotation)
				as GameObject;
				Rigidbody rigPeluruPlayer = peluruPlayer.GetComponent<Rigidbody>();
				rigPeluruPlayer.AddForce(rigPeluruPlayer.transform.forward * kecPeluru);
			}
		// }
	}
	void Pisau(){
		Debug.Log("serangan pisau");
	}
	void Granat(){
		Debug.Log("serangan granat");
	}
	private void OnTriggerEnter(Collider player){
		if(player.tag == "Bot")
		{
			health -= 10;
			// healthBar.value = health;
			if(health <= 0)
			{
				print("player is death");
				Destroy(player.gameObject);
				animator.SetTrigger("isMati");}
			// }else{
			// 	animator.SetBool("isDie", false);
			// }
		}
	}
}
