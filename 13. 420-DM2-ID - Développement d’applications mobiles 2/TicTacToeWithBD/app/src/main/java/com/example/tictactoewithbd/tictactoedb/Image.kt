package com.example.tictactoewithbd.tictactoedb

import androidx.room.ColumnInfo
import androidx.room.Entity
import androidx.room.PrimaryKey
import java.io.Serializable

@Entity(tableName = "image_table")
data class Image(@PrimaryKey @ColumnInfo(name = "name") val name:String,
                 @ColumnInfo(name = "path") val path:Int) : Serializable
