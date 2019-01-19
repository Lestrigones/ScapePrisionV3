/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public Map map;
    public M1 map1;
    public M2 map2;
    public M3 map3;
    public GameObject exit;                                         //Prefab to spawn for exit.
    public GameObject[] floorTiles;                                 //Array of floor prefabs.
    public GameObject[] outerWallTiles;                                  //Array of wall prefabs.
    public GameObject[] foodTiles;                                  //Array of food prefabs.
    public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
                                                                    //public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.

    private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
    private List<Vector2> gridPositions = new List<Vector2>();   //A list of possible locations to place tiles.



    /*              Nada 				0
                   Salida mapa			z

                   Objetos preso
                           LLave				a
                           Navaja				b
                           Cuerda				c


                   Posiciónes iniciales
                           Jugador				1
                           Enemigo mapa 1		2
                           Enemigos mapa 2		3
                           Enemigos mapa 3		4

       String mapa 1
       0000000000000000/0010000000000000/0000000000000000/0000000000000000/0000000000000000/00a0000000000200/0000000000000000/000000000000000z/0000000000000000

       String mapa 2
       0000000000000000/00b0000000000300/0000000000000000/0000000000000000/0000000000000000/0000000000000000/000000000000000z/1000000000000300/0000000000000000

       String mapa 3
       0000000000000000/00000c004000000z/000000000000000z/000000000000000z/000000000000000z/000000000000000z/000000000000000z/100000004000000z/0000000000000000

    */

   /* //Aqui llamaremos a la API para que nos de un mapa.
    void InitialiseMap()
    {
        int id, numFilas, numColumnas, numObjetos, numPolicia, numJugador;
        id = 1;
        numFilas = 9;
        numColumnas = 16;
        numObjetos = 3;
        numPolicia = 5;
        numJugador = 1;
        this.map = gameObject.AddComponent<Map>();
        this.map.setIdmap(id);
        this.map.setNumfilas(numFilas);
        this.map.setNumcolumnas(numColumnas);
        this.map.setNumobjetos(numObjetos);
        this.map.setNumpolicia(numPolicia);
    }
    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = 1; x < this.map.getNumcolumnas(); x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 1; y < this.map.getNumfilas() - 1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }


    //Sets up the outer walls and floor (background) of the game board.
    void BoardSetup()
    {
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;
        //Para colocar la salida mas o menos en la mitad
        int salidaPosX = this.map.getNumcolumnas() / 2;
        GameObject.Find("Hero").transform.position = new Vector3(salidaPosX, 1, 0f);
        //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
        for (int x = -1; x < this.map.getNumcolumnas() + 1; x++)
        {
            //Loop along y axis, starting from -1 to place floor or outerwall tiles.
            for (int y = -1; y < this.map.getNumfilas() + 1; y++)
            {
                //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                GameObject toInstantiate;
                

                //Suelo
                toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];


                //Paredes
                if (x == -1 || x == this.map.getNumcolumnas() || y == -1 || y == this.map.getNumfilas())
                {
                    if (x == -1 && y == this.map.getNumfilas())
                    {
                        //en la posicion 3 es donde se guarda este prefab, ir a gamemanager en unity y comprobar.
                        toInstantiate = outerWallTiles[3];
                    }
                    else if (x == this.map.getNumcolumnas() && y == this.map.getNumfilas())
                    {
                        toInstantiate = outerWallTiles[2];
                    }
                    else if (x == -1 && y == -1)
                    {
                        toInstantiate = outerWallTiles[1];
                    }
                    else if (x == this.map.getNumcolumnas() && y == -1)
                    {
                        toInstantiate = outerWallTiles[0];
                    }
                    else if (x == -1 && y == 0)
                    {
                        toInstantiate = outerWallTiles[5];
                    }
                    else if (x == this.map.getNumcolumnas() && y == 0)
                    {
                        toInstantiate = outerWallTiles[4];
                    }
                    else if (x == -1)
                    {
                        toInstantiate = outerWallTiles[9];
                    }
                    else if (x == this.map.getNumcolumnas())
                    {
                        toInstantiate = outerWallTiles[8];
                    }
                    else if (x == salidaPosX && y == -1)
                    {
                        toInstantiate = exit;
                    }
                    else
                    {
                        toInstantiate = outerWallTiles[6];
                    }

                }

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;

                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //RandomPosition returns a random position from our list gridPositions.
    Vector2 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector2 randomPosition = gridPositions[randomIndex];

        //Remove the entry at randomIndex from the list so that it can't be re-used.
        gridPositions.RemoveAt(randomIndex);

        //Return the randomly selected Vector2 position.
        return randomPosition;
    }


    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        int objectCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < this.map.getNumobjetos(); i++)
        {
            //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
            Vector2 randomPosition = RandomPosition();

            //Choose a random tile from tileArray and assign it to tileChoice
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

            //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }


    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene()
    {
        //get the information of the Map from the API
        InitialiseMap();
        //Creates the outer walls and floor.
        BoardSetup();
        //Reset our list of gridpositions.
        InitialiseList();

        //Instantiate a number of food tiles based on the API, at randomized positions.
        LayoutObjectAtRandom(foodTiles, this.map.getNumobjetos(), this.map.getNumobjetos());

        //Instantiate a number of enemies, at randomized positions.
        LayoutObjectAtRandom(enemyTiles, this.map.getNumenemigos(), this.map.getNumenemigos());
    }
}*/