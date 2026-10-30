using UnityEngine;

namespace Chapter.Singleton
{
    public class AudioManager : Singleton<AudioManager>
    {
        GameObject player;

        public AudioClip jumpSound;
        public AudioClip deathSound;
        public AudioClip winSound;
        public AudioClip enemyDeathSound;

        public AudioSource audioSource;

        // Update is called once per frame
        void Update()
        {
            player = GameObject.Find("Player");
            audioSource = FindAnyObjectByType<AudioSource>();

            if (Input.GetKeyDown(KeyCode.Space) && player.GetComponent<Movement>().isGrounded == true && player.GetComponent<Movement>().isDead == false && player.GetComponent<Movement>().gameManager.isPaused == false)
            {
                audioSource.PlayOneShot(jumpSound);
            }
            if (player.GetComponent<Movement>().jumpedEnemy == true)
            {
                audioSource.PlayOneShot(jumpSound);
                audioSource.PlayOneShot(enemyDeathSound);
            }

            if(player.GetComponent<Movement>().isDead == true)
            {
                audioSource.PlayOneShot(deathSound);
            }

            if(player.GetComponent<Movement>().hasWon == true)
            {
                audioSource.PlayOneShot(winSound);
            }
        }
    }

}