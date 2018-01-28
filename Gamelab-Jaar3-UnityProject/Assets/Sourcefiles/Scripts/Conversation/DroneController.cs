using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Actors.Actor character;
    public Transform player;

    float rotateSpeed;

    public Material[] mats;

    Renderer mat;
    public Renderer holoMat;

    public ParticleSystem[] hologramParticles;



    void Awake()
    {
        player = GameObject.Find("Player").transform;
        mat = GetComponent<Renderer>();
        rotateSpeed = 1;
    }

	// Use this for initialization
	void Start ()
    {
        var main = hologramParticles[0].main;
        var main2 = hologramParticles[1].main;


        switch  (character)
        {
            case Actors.Actor.Dust:
                mat.material = mats[0];
                break;

            case Actors.Actor.Blaze:
                mat.material = mats[1];
                main.startColor = new ParticleSystem.MinMaxGradient(new Color32(0xcc, 0x52, 0x00, 0xFF));
                main2.startColor = new ParticleSystem.MinMaxGradient(new Color32(0xcc, 0x52, 0x00, 0xFF));
                break;

            case Actors.Actor.Livie:
                mat.material = mats[2];
                main.startColor = new ParticleSystem.MinMaxGradient(new Color32(0x00, 0x86, 0xb3, 0xFF));
                main2.startColor = new ParticleSystem.MinMaxGradient(new Color32(0x00, 0x86, 0xb3, 0xFF));
                break;

            case Actors.Actor.Violet:
                mat.material = mats[3];
                break;

            case Actors.Actor.Scarlet:
                mat.material = mats[4];
                break;

            case Actors.Actor.Marine:
                mat.material = mats[5];
                break;

            case Actors.Actor.Moss:
                mat.material = mats[6];
                main.startColor = new ParticleSystem.MinMaxGradient(new Color32(0xe2, 0xf4, 0xf4, 0xFF));
                main2.startColor = new ParticleSystem.MinMaxGradient(new Color32(0xe2, 0xf4, 0xf4, 0xFF));
                break;

            case Actors.Actor.Auburn:
                mat.material = mats[7];
                break;

            case Actors.Actor.Ash:
                mat.material = mats[8];
                break;

            case Actors.Actor.Amber:
                mat.material = mats[9];
                break;

            case Actors.Actor.Unknown:
                mat.material = mats[10];
                break;
        }
        holoMat.material = Resources.Load("HologramPortraits/Holo_" + character.ToString(), typeof(Material)) as Material;


    }
	

	// Update is called once per frame
	void Update ()
    {
        FollowPlayer();
	}

    void FollowPlayer()
    {
        transform.parent.LookAt(player);
    }

}
