package com.example.tictactoewithbd.tictactoedb

import androidx.room.Dao
import androidx.room.Insert
import androidx.room.OnConflictStrategy
import androidx.room.Query
import kotlinx.coroutines.flow.Flow

@Dao
interface ImageDao {

    @Query("SELECT * from image_table")
    fun getAllImages(): Flow<List<Image>>

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun insert(image: Image)

    @Query("DELETE FROM image_table")
    suspend fun deleteAll()
}