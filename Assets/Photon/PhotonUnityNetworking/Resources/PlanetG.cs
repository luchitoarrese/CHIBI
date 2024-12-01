using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetG : MonoBehaviour
{


    [SerializeField] private GameObject[] planets;
    public float velocidad = 3;
    [SerializeField] private float velRotation = 100;
    public Rigidbody rbd;
    float fuerzaSalto = 50000;
    private float x,y;

    [Header("Configuración de la Cámara")]
    [SerializeField] private Transform camara; // Asigna la cámara aquí
    [SerializeField] private float suavizadoCamara = 2f;
    private Vector3 posicionRelativaCamara;

    void Start()
    {
        
        if (camara != null)
        {
            posicionRelativaCamara = camara.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaCercana = Vector3.Distance(transform.position, planets[0].transform.position);
        int planetaCercano = 0;
        for (int i = 0; i < planets.Length; i++)
        {
            float distaux = Vector3.Distance(transform.position, planets[i].transform.position);
            if (distaux < distanciaCercana) { distanciaCercana = distaux; planetaCercano = i; }
            if ( planetaCercano > 0)
            {
                Quaternion targetCamRotation = Quaternion.LookRotation(transform.forward, transform.up);
                camara.rotation = Quaternion.Slerp(camara.rotation, transform.rotation, Time.deltaTime * suavizadoCamara);
            }

        }


        Physics.gravity = planets[planetaCercano].transform.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, -Physics.gravity) * transform.rotation;

            if (Input.GetKey(KeyCode.W)) { transform.Translate(new Vector3(-velocidad * Time.deltaTime, 0, 0));}
            if (Input.GetKey(KeyCode.S)) { transform.Translate(new Vector3(velocidad * Time.deltaTime, 0, 0));}
            

            if (Input.GetKey(KeyCode.A)) { transform.Rotate(0, -velRotation * Time.deltaTime, 0); }
            if (Input.GetKey(KeyCode.D)) { transform.Rotate(0, velRotation * Time.deltaTime, 0); }
            if (Input.GetKeyDown(KeyCode.Space)) { rbd.AddForce(transform.up * fuerzaSalto); }

       
    }
    }
