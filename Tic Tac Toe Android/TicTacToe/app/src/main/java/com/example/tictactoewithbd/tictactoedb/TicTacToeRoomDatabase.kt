package com.example.tictactoewithbd.tictactoedb

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import androidx.sqlite.db.SupportSQLiteDatabase
import com.example.tictactoewithbd.R
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch
import kotlin.concurrent.Volatile

@Database(entities = arrayOf(Player::class, Image::class), version = 1, exportSchema = false)
abstract class TicTacToeRoomDatabase : RoomDatabase() {

    abstract fun playerDao(): PlayerDao
    abstract fun ImageDao(): ImageDao

    private class TicTacToeDatabaseCallback(
        private val scope: CoroutineScope
    ) : RoomDatabase.Callback(){
        override fun onCreate(db: SupportSQLiteDatabase) {
            super.onCreate(db)
            INSTANCE?.let { database ->
                scope.launch {
                    populatePlayer(database.playerDao())
                    populateImage(database.ImageDao())
                }
            }
        }

        suspend fun populateImage(imageDao: ImageDao) {
            imageDao.deleteAll()

            var image = Image("Cross", R.drawable.cross)
            imageDao.insert(image)
            image = Image("Circle", R.drawable.circle)
            imageDao.insert(image)
            image = Image("Heart", R.drawable.heart)
            imageDao.insert(image)
            image = Image("Star", R.drawable.star)
            0
            imageDao.insert(image)
        }

        suspend fun populatePlayer(playerDao: PlayerDao) {
            playerDao.deleteAll()

            var player = Player("Marc-André", 0)
            playerDao.insert(player)
            player = Player("Boualem", 0)
            playerDao.insert(player)
        }


    }

    companion object{
        @Volatile
        private var INSTANCE: TicTacToeRoomDatabase? = null

        fun getDatabase(context: Context,scope: CoroutineScope): TicTacToeRoomDatabase{
            return INSTANCE ?: synchronized(this){
                val instance = Room.databaseBuilder(
                    context.applicationContext,
                    TicTacToeRoomDatabase::class.java,
                    "tictactoe_database"
                ).addCallback(TicTacToeDatabaseCallback(scope)).build()
                INSTANCE = instance
                instance
            }
        }
    }

}