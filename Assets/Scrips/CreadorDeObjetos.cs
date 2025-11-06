using UnityEngine;

public class CreadorDeObjetos : MonoBehaviour
{
     //Si tengo la referencia del objeto
        
            //Posicion del raton Input.mousePosition (Vector3 la posicion del raton)
            //Debug.Log(Input.mousePosition);
            
            //Ray con informacion desde la pantalla al punto en el que esta el raton
            //OJO: el rayo no se ejecuta por si solo
            
            //Puedo dibujar mi rayo en la escena para interpretar visualmetnte que esta sucediendo
            //esta funcion se le dice el origen y la direccion (el *100 es para que tenga 100 metros de longitud)
            //y un color
            
            //RaycasHit es un tipo de dato que almacena la informacion sobre
            //la colision (si es que existe del rayo), podre saber la posicion, objeto con el que choca...
            


            //Para lanzar el rayo y hacer cosas necesito llamar a Physics.Raycast
            //Se le pasa el rayo que he creado mas arriba
            //out hit<- para que en hit almacene la informacion
            //100f la longitud


    [SerializeField]
    GameObject[] prefabs;
    [SerializeField]
    Material[] materials;

    int currentPrefab = 0;
    GameObject currentGameObject = null;

    // Update is called once per frame
    void Update()
    {
        ComprobarTeclado();
        CrearObjecto();
        MoverObject();
        CambiarMaterial();
        ComprobarClick();
    }
    void ComprobarClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentGameObject = null;
        }
    }
    void MoverObject()
    {
        if (currentGameObject != null)
        {

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            currentGameObject.SetActive(false);
            if (Physics.Raycast(myRay, out hit, 100f))
            {
                currentGameObject.transform.position = hit.point + Vector3.up;
            }

            currentGameObject.SetActive(true);
        }
    }
    void CambiarMaterial()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            currentGameObject.GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Length)];
        }

    }
    void CrearObjecto()
    {
        if (currentGameObject == null)
        {
            currentGameObject = Instantiate(prefabs[currentPrefab]);
            currentGameObject.transform.position = Vector3.zero;
        }
    }

    void ComprobarTeclado()
    {
        int oldNumber = currentPrefab;
        if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.Keypad1))
        {
            currentPrefab = 0;
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.Keypad2))
        {
            currentPrefab = 1;
        }
        if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.Keypad3))
        {
            currentPrefab = 2;
        }

        if (currentGameObject != null && oldNumber != currentPrefab)
        {
            DestroyImmediate(currentGameObject);
        }

    }
}    

