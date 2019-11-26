using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

	private Animator anim;
	public Transform pemain;
	TriggerPatrol tPatrol;
	public int posisi_titik;
	public float stopDistance;
	// public bool showDebug;
	// [HideInInspector]
	public GameObject target;

	// [Header("Nav Mesh Stuff")]
	private NavMeshAgent nav;
	public Transform[] point;
	int disPoint = 0; 
	public float speed = 3.0f;
	public float jarak;
	public int ammo;
	public int healthEnemy = 200;
	// public Slider healthBar;
	public Transform munculAmmoNPC;
	public GameObject peluruNPC;
	// public ParticleSystem muzzle;
	public AudioSource tembakanNPC;

	[Header("Ranges")]
	public float AggroRadius;
	public float AttackDistance;
	public float AttackCoolDown;
	float StartTimer;
	bool attacking = false;
	public float AIdurasi = 5f;
	float TaiDurasi;


	void OnEnable(){
		tPatrol = GetComponentInChildren<TriggerPatrol>();
		tPatrol.InRadius = AggroRadius;
	}
	void Start () {
		TaiDurasi = AIdurasi;
		anim = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();
		nav.speed = speed;
		nav.autoBraking = false;
		nav.stoppingDistance = stopDistance;
		StartTimer = AttackCoolDown;
	}
	void Update () {
		// waypoint
		float distance = Vector3.Distance(transform.position, point[posisi_titik].position);
		transform.LookAt(point[posisi_titik]);

		if(distance <= stopDistance)
		{
			posisi_titik +=1;
		}
		if(posisi_titik >= point.Length)
		{
			posisi_titik = 0;
		}
		if(posisi_titik == 0)
		{
			nav.SetDestination(point[0].position);
		}
		if(posisi_titik == 1)
		{
			nav.SetDestination(point[1].position);
		}
		if(posisi_titik == 2)
		{
			nav.SetDestination(point[2].position);
		}
		// 
		transform.LookAt(pemain);

			float speed = nav.velocity.magnitude;
			anim.SetFloat("Speed", speed);
		// }
		if(attacking == true)
		{
			StartTimer += Time.deltaTime;
			if(StartTimer >= AttackCoolDown){
				StartTimer = 0f;
				Attack();
			}
		}
		if(target != null)
		{
			CekDistance();
		}	
	}
	void CekDistance(){
		float dist = Vector3.Distance(transform.position, target.transform.position);
		if(dist <= AttackDistance){
			// attacking = true;
			Attack();
			nav.isStopped = true;
		}else{
			nav.isStopped = false;
			nav.destination = target.transform.position;
			attacking = false;
		}
	}
	void Attack(){

		int aksi = 0;
		if(AIdurasi >= TaiDurasi){
			//  aksi = GameObject.Find("Pemain").GetComponent<DataSet>().KirimAIData();
			AIdurasi -= Time.deltaTime;
		}else{
			AIdurasi -= Time.deltaTime;
			if(AIdurasi < 0f){
				AIdurasi =TaiDurasi;
			}
			
		}
		
		if(aksi == 0){
			tembakanNPC.Play();
		transform.LookAt(pemain);
		// print("attacking pemain");
		StartTimer -= Time.deltaTime;
		if(StartTimer <= 0)
		{
			StartTimer = AttackCoolDown;
			GameObject tembakNPC = Instantiate(peluruNPC, munculAmmoNPC.transform.position,
			munculAmmoNPC.rotation) as GameObject;
			Rigidbody rbAmmoBot = tembakNPC.GetComponent<Rigidbody>();
			rbAmmoBot.AddForce(rbAmmoBot.transform.forward * 1800);
		}
		}else{
			Debug.Log("jurus");
		}
			// muzzle.Play();
		
	}
	void Damage(){

	}
	private void OnTriggerEnter(Collider bot){
		if(bot.tag == "Damage")
		{
			// Instantiate(Resources.Load("Blood"), bot.transform.position, bot.transform.rotation);
			Destroy(bot.gameObject);
			healthEnemy -= 10;
			// healthBar.value = health;
			// print("Bot is touch, 20");
			if(healthEnemy <= 0)
			{
				print("Bot is death");
			}
		}
	}
}
