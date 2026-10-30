using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Chapter.Singleton
{
    public class GameManager : Singleton<GameManager>
    {
        GameObject player;
        public GameObject goal;
        public EnemySpawner spawner;
        public bool isPaused = false;
        public float timer = 0f;
        private ICommand pauseCommand;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            //Enemy enemy = spawner.SpawnEnemy();
            pauseCommand = new ActionCommands(TogglePause);
        }

        // Update is called once per frame
        void Update()
        {
            player = GameObject.Find("Player");
            spawner = FindAnyObjectByType<EnemySpawner>();
            goal = GameObject.Find("Goal");

            SpawnLoop();
            deathRestart();
            winSequence();

            if(!KeyRebineMenu.isOpen && Input.GetKeyDown(KeyBindings.Pause))
            {
                pauseCommand.Execute();
            }
        }

        public void deathRestart()
        {
            if (Input.GetKeyDown(KeyBindings.Restart) && player.GetComponent<Movement>().isDead == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                player.GetComponent<Movement>().isDead = false;
            }
        }

        public void SpawnLoop()
        {
            timer += Time.deltaTime;
            if(timer >= 3)
            {
                Enemy enemy = spawner.SpawnEnemy();
                timer = 0;
            }
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
        }

        public void winSequence()
        {
            if (player.GetComponent<Movement>().hasWon == true)
            {
                Debug.Log("You Win!");
            }
        }
        public void TogglePause()
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}