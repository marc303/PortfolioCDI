package com.example.tictactoewithbd.tictactoedb

import androidx.annotation.WorkerThread
import kotlinx.coroutines.flow.Flow

class ImageRepository(private val imageDao: ImageDao) {

    val allImages: Flow<List<Image>> = imageDao.getAllImages()

    @Suppress("RedundantSuspendModifier")
    @WorkerThread
    suspend fun insert(image: Image){
        imageDao.insert(image)
    }
}