﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControllerScript : NetworkBehaviour {

	//VARs
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	//FUNCs
	void Update(){

		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
	}

	[Command]
	void CmdFire(){
		var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6;

		NetworkServer.Spawn (bullet);

		Destroy (bullet, 2.0f);
	}

	public override void OnStartLocalPlayer(){
	
		GetComponent<MeshRenderer> ().material.color = Color.blue;

	}
}
