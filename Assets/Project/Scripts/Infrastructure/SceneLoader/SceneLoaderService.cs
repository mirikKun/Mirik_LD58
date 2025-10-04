
using UnityEngine.SceneManagement;

namespace Project.Scripts.Infrastructure.SceneLoader
{
    public class SceneLoaderService
    {


        
        public static void InstantLoad(string name)
        {
            SceneManager.LoadScene(name);
        }
    
 
    }
}