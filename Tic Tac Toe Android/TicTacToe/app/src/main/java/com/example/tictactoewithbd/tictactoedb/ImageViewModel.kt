package com.example.tictactoewithbd.tictactoedb

import androidx.lifecycle.LiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.asLiveData
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.launch

class ImageViewModel(private val repository: ImageRepository) : ViewModel() {

    val allImages: LiveData<List<Image>> = repository.allImages.asLiveData()

    fun insert(image: Image) = viewModelScope.launch {
        repository.insert(image)
    }
}

class ImageViewModelFactory(private val repository: ImageRepository) : ViewModelProvider.Factory{
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(ImageViewModel::class.java)){
            @Suppress("UNCHECKED_CAST")
            return ImageViewModel(repository) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}