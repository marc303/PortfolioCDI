package com.example.tictactoe

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import com.example.tictactoe.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState);
        binding = ActivityMainBinding.inflate(layoutInflater);
        setContentView(binding.root);
    }

    fun startGame(view : View)
    {
        var nameJ1 = binding.textJoueur1.text.toString();
        var nameJ2 = binding.textJoueur2.text.toString();

        if (nameJ1.equals("") && !nameJ2.equals(""))
        {
            Toast.makeText(this, "Veuillez saisir le nom du Joueur 1!", Toast.LENGTH_LONG).show();
        }
        else if (!nameJ1.equals("") && nameJ2.equals(""))
        {
            Toast.makeText(this, "Veuillez saisir le nom du Joueur 2!", Toast.LENGTH_LONG).show();
        }
        else if (nameJ1.equals("") && nameJ2.equals(""))
        {
            Toast.makeText(this, "Veuillez saisir le nom des joueurs!", Toast.LENGTH_LONG).show();
        }
        else
        {
            var intent = Intent(this, SecondActivity::class.java);
            var player1 = Player(nameJ1, 0);
            var player2 = Player(nameJ2, 0);
            intent.putExtra("Player1", player1);
            intent.putExtra("Player2", player2);
            startActivity(intent);
        }
    }
}