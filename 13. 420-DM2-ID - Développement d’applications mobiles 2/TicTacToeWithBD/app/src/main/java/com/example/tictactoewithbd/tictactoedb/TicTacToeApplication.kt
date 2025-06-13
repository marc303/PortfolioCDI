package com.example.tictactoewithbd.tictactoedb

import android.app.Application
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.SupervisorJob

class TicTacToeApplication : Application() {
    val applicationScope = CoroutineScope(SupervisorJob())

    val database by lazy { TicTacToeRoomDatabase.getDatabase(this, applicationScope) }
    val player_repository by lazy { PlayerRepository(database.playerDao()) }
    val image_repository by lazy { ImageRepository(database.ImageDao()) }

}