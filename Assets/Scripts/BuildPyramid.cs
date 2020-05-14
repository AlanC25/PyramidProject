using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPyramid : MonoBehaviour
{
    public GameObject PyramidBlock;
    public GameObject Pavement;
    public GameObject CatStatue;

    // Start is called before the first frame update
    void Start()
    {
        //Select x and z size
        int length = 100;

        //Place CatStatues(CS)
        for (int csx = (-length - 15); csx <= (length + 15); csx += 10)
        {
            for (int csz = (-length - 15); csz <= (length + 15); csz += 10)
            {
                if ((csx <= (-length - 15) || csx >= (length + 15)) || (csz <= (-length - 15) || csz >= (length + 15)))
                {
                    Instantiate(CatStatue);
                    CatStatue.transform.position = new Vector3(csx, 0.5f, csz);
                    //Rotate to face out
                    if (csx < -length)
                    {
                        CatStatue.transform.rotation = Quaternion.Euler(0, -90, 0);
                    }
                    else if (csx > length)
                    {
                        CatStatue.transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (csz < -length)
                    {
                        CatStatue.transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (csz > length)
                    {
                        CatStatue.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
        }

        //Build path around pyramid
        //Set overall area, pyramid length + 35 on x side, PathX(PX)
        for (int px = (-length - 35); px <= (length + 35); px++)
        {
            //pyranid length + 20 on z side, PathZ(PZ) 
            for (int pz = (-length - 35); pz <= (length + 35); pz++)
            {
                //exclude land covered by pyramid
                if ((px < -length || px > length) || (pz < -length || pz > length))
                {
                    Instantiate(Pavement);
                    Pavement.transform.position = new Vector3(px, 0.5f, pz);
                }
            }
        }

        //Build pyramid by layers starting at bottom, also sets height/number of layers, height should match length
        for (int height = 0; height <= 100; height += 5)
        {
            //start on x's (-) edge, loop til (+) edge
            for (int x = -length; x <= length; x += 5)
            {
                //start on z's (-) edge, loop til (+) edge
                for (int z = -length; z <= length; z += 5)
                {
                    //only if edge position selected, keeps pyramid hollow
                    if (Mathf.Abs(x) == length || Mathf.Abs(z) == length)
                    {
                        //Use prefab to create GameObject(greater control than using prefab directly)
                        GameObject Brick = Instantiate(PyramidBlock) as GameObject;
                        
                        //small size and rotation variations for worn appearance
                        Brick.transform.localScale = new Vector3(Random.Range(4.9f, 5.1f), Random.Range(4.9f, 5.1f), Random.Range(4.9f, 5.1f));
                        Brick.transform.rotation = Quaternion.Euler(0, Random.Range(-2.5f, 2.5f), 0);
                        
                        //select new position
                        Brick.transform.position = new Vector3(x, height, z);
                        
                        //Randomise block shade
                        Brick.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.8f, 0.9f), Random.Range(0.6f, 0.7f), 0, 1);
                    }
                }
            }
            //Reduce size by size of one block each layer. Must go last so other placemets can use lengths initial value
            length = length - 5;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
