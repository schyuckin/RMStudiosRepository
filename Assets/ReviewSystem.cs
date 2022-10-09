using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewSystem : MonoBehaviour
{
    private string sentence1;
    private string sentence2;
    private string sentence3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    void CheckIfCorrect()
    {
        //check both scripts
    }

    private void Review()
    {
        if ( /*color not correct*/)
        {
            sentence1 = "This is not what I asked for";
        }
        else
        {
            sentence1 = "It basically worked";
        }

        if ( /*power not correct*/)
        {
            if ( /*case for each potion type to have unique review*/)
            {
                
            }
        }
        else
        {
            sentence2 = "it worked exactly as expected";
        };

        if (/* sigil not correct */)
        {
            if ( /*case for each potion type to have unique review*/)
            {
                
            }
        }
        else
        {
            sentence3 = "I did not ask for the potion to cause the growth of a third eye";
        }
    } 
}
