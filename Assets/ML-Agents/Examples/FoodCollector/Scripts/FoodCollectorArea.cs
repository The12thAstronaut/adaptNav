using UnityEngine;
using Unity.MLAgentsExamples;
using System.Collections;

public class FoodCollectorArea : Area
{
    public GameObject food;
    public GameObject badFood;
    public int numFood;
    public int numBadFood;
    public bool respawnFood;
    public float range;
    public int veloctiyScalar;

    void CreateFood(int num, GameObject type)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject f = Instantiate(type, new Vector3(Random.Range(-range, range), 1f,
                Random.Range(-range, range)) + transform.position,
                Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 90f)));
            //Instantiate rigid body with velocity
            f.GetComponent<Rigidbody>().velocity = veloctiyScalar * -1 * transform.localScale.x * f.transform.forward;
            f.GetComponent<FoodLogic>().respawn = respawnFood;
            f.GetComponent<FoodLogic>().myArea = this;

        }
    }

    public IEnumerator ResetFoodArea(GameObject[] agents)
    {
        CreateFood(numBadFood, badFood);
  
        print("Hiding agents");
        foreach (GameObject agent in agents)
        {
            agent.SetActive(false);
        }

        yield return new WaitForSeconds(10);
        print("Spawning agents");
        foreach (GameObject agent in agents)
        {
            agent.SetActive(true);
            if (agent.transform.parent == gameObject.transform)
            {
                agent.transform.position = new Vector3(Random.Range(-range, range), 2f,
                    Random.Range(-range, range))
                    + transform.position;
                agent.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
            }
        }

        // No longer spawn good food, only poison
        // CreateFood(numFood, food);
    }

    public override void ResetArea()
    {
    }
}
