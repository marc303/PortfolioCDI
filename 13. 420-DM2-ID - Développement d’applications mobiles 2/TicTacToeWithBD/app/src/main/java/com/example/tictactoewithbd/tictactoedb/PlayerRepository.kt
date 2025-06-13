package com.example.tictactoewithbd.tictactoedb

import androidx.annotation.WorkerThread
import kotlinx.coroutines.flow.Flow

class PlayerRepository(private val playerDao: PlayerDao) {

    val allPlayers: Flow<List<Player>> = playerDao.getAlphabetizedPlayer()

    @Suppress("RedundantSuspendModifier")
    @WorkerThread
    suspend fun insert(player: Player){
        playerDao.insert(player)
    }

    @WorkerThread
    suspend fun deleteAll(){
        playerDao.deleteAll()
    }

    @WorkerThread
    suspend fun updatePlayer(name:String, score:Int) {
        playerDao.updatePlayer(name, score)
    }
}