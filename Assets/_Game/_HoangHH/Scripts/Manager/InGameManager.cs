using System;
using System.Collections.Generic;
using DG.Tweening;
using HoangHH.Data.PrefabData;
using HoangHH.DesignPattern;
using HoangHH.InGame;
using HoangHH.InGame.Board;
using HoangHH.InGame.Character.Base;
using HoangHH.InGame.Grid;
using HoangHH.Utilities.Grid;
using MEC;
using UnityEngine;

namespace HoangHH.Manager
{
    public class InGameManager : Singleton<InGameManager>
    {
        private Level _currentLevel;

        private void Update()
        {
            // Reset the Scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }

        protected override void SingletonStarted()
        {
            // Create new GameGrid
            GameGrid gameGrid = new(10, 10, 1, default, () => new GameGridCell(), GridPlane.XZ);
            // Log
            HLog.Log(DevID.Hoang, "GameGrid created with size: " + gameGrid.Width + "x" + gameGrid.Height + " and cell size: " + gameGrid.CellSize);
            // Create Sphere Object at each cell position
            Timing.RunCoroutine(TestSpawnBoard(gameGrid, 3f, () => TestSpawnPlayer(gameGrid, 5, 5)));
        }

        private static void TestSpawnPlayer(GameGrid gameGrid, int x, int y)
        {
            // Check if the cell is valid
            if (gameGrid.IsValidCell(x, y))
            {
                // Get the cell position
                Vector3 worldPos = new Vector3(0, HConstant.CHARACTER_SPAWN_Y, 0) + gameGrid.GridArray[x, y].WorldPos;
                // Spawn the player
                IPlayer p = DataManager.Instance.SpawnData<IPlayer, PlayerType>(DataType.Player, PlayerType.Normal, worldPos);
                // Add the player to the cell
                gameGrid.GridArray[x, y].AddCharacter(p as BCharacter);
            }
            else
            {
                HLog.Log(DevID.Hoang, "Invalid cell position");
            }
        }
        
        private static IEnumerator<float> TestSpawnBoard(GameGrid gameGrid, float spawnTime, Action callback = null)
        {
            // Calculate time to spawn object
            float timeToSpawn = spawnTime / (gameGrid.Width * gameGrid.Height);
            bool isStartFromLeft = true;
            BoardType boardType = BoardType.NormalBlack;
            for (int y = gameGrid.Height - 1; y >= 0; y--)
            {
                if (isStartFromLeft)
                {
                    isStartFromLeft = false;
                    for (int x = 0; x < gameGrid.Width; x++)
                    {
                        Vector3 worldPos = new Vector3(0, HConstant.BOARD_SPAWN_Y, 0) +gameGrid.GridArray[x, y].WorldPos;
                        BBoard board = DataManager.Instance.SpawnData<BBoard, BoardType>(DataType.Board, boardType, worldPos);
                        boardType = boardType == BoardType.NormalBlack ? BoardType.NormalWhite : BoardType.NormalBlack;
                        // Scaling & Rotating object from 0 to 1
                        board.Tf.localScale = Vector3.zero;
                        Sequence sequence = DOTween.Sequence();
                        sequence.Append(board.Tf.DOScale(Vector3.one, 0.3f));
                        sequence.Join(board.Tf.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.FastBeyond360));
                        sequence.Play();
                        yield return Timing.WaitForSeconds(timeToSpawn);
                    }
                }
                else
                {
                    isStartFromLeft = true;
                    for (int x = gameGrid.Width - 1; x >= 0; x--)
                    {
                        Vector3 worldPos = new Vector3(0, HConstant.BOARD_SPAWN_Y, 0) + gameGrid.GridArray[x, y].WorldPos;
                        BBoard board = DataManager.Instance.SpawnData<BBoard, BoardType>(DataType.Board, boardType, worldPos);
                        boardType = boardType == BoardType.NormalBlack ? BoardType.NormalWhite : BoardType.NormalBlack;
                        // Scaling & Rotating object from 0 to 1
                        board.Tf.localScale = Vector3.zero;
                        Sequence sequence = DOTween.Sequence();
                        sequence.Append(board.Tf.DOScale(Vector3.one, 0.3f));
                        sequence.Join(board.Tf.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.FastBeyond360));
                        sequence.Play();
                        yield return Timing.WaitForSeconds(timeToSpawn);
                    }
                }
                
            }
            callback?.Invoke();
        }
    }
}
