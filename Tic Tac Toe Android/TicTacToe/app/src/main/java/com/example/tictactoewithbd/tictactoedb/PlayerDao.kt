package com.example.tictactoewithbd.tictactoedb

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import androidx.room.Update
import kotlinx.coroutines.flow.Flow

@Dao
interface PlayerDao {

    @Query("SELECT * from player_table ORDER BY name ASC")
    fun getAlphabetizedPlayer(): Flow<List<Player>>

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun insert(player: Player)

    @Query("DELETE FROM player_table")
    suspend fun deleteAll()

    @Query("UPDATE player_table SET score = :scoreResult WHERE name = :nameResult")
    suspend fun updatePlayer(nameResult:String, scoreResult: Int) : Int
}