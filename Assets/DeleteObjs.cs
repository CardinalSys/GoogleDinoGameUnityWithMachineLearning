using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjs : MonoBehaviour
{
    public bool mlAgent = false;
    public ScenarioMananger sm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if (!mlAgent)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.transform.position = new Vector3(0, 30, -1);
            }

            /*
            switch (collision.name)
            {
                case "1":
                    sm.cactusNum = 0;
                    break;
                case "2":
                    sm.cactusNum = 1;
                    break;
                case "3":
                    sm.cactusNum = 2;
                    break;
                case "4":
                    sm.cactusNum = 3;
                    break;
            }
            */
        }

    }
}
