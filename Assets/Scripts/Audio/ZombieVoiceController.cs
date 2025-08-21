using System.Threading.Tasks;
using UnityEngine;

namespace ZRAudio
{
    //HARDCODE
    public class ZombieVoiceController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource[] voices;

        private async void Start()
        {
            await Task.Delay(6000);

            foreach (var voice in voices)
            {
                voice.Play();
                await Task.Delay(Random.Range(3, 7) * 1000);
            }
        }
    }
}
