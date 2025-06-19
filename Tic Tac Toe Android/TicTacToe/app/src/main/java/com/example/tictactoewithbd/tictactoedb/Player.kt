package com.example.tictactoewithbd.tictactoedb

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import java.io.Serializable

@Entity(tableName = "player_table")
data class Player(@PrimaryKey @ColumnInfo(name = "name") val name:String,
                  @ColumnInfo(name = "score") var score:Int) : Serializable
