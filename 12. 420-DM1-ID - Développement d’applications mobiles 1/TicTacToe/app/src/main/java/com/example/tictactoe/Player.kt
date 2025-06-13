package com.example.tictactoe

import java.io.Serializable

class Player() : Serializable{

    var player_name: String = ""
        get()
        {
            return field;
        }
        set(value)
        {
            field = value;
        }

    var player_score: Int = 0
        get()
        {
            return field;
        }
        set(value)
        {
            field = value;
        }
    constructor(name: String, score: Int) : this() {
        this.player_name = name;
        this.player_score = score;
    }
}