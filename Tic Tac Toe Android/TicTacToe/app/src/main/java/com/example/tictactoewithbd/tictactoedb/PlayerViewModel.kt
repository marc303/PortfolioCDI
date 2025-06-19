package com.example.tictactoewithbd.tictactoedb

import androidx.lifecycle.LiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.asLiveData
import androidx.lifecycle.viewModelScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class PlayerViewModel(private val repository: PlayerRepository) : ViewModel() {

    val allPlayers: LiveData<List<Player>> = repository.allPlayers.asLiveData()

    fun insert(player: Player) = viewModelScope.launch() {
        repository.insert(player)
    }

    fun deleteAll() = viewModelScope.launch {
        repository.deleteAll()
    }

    fun updatePlayer(name:String, score:Int) = viewModelScope.launch() {
        repository.updatePlayer(name,score)
    }
}

class PlayerViewModelFactory(private val repository: PlayerRepository) : ViewModelProvider.Factory{
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(PlayerViewModel::class.java)){
            @Suppress("UNCHECKED_CAST")
            return PlayerViewModel(repository) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}