package com.example.tictactoe

import android.content.Intent
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.graphics.drawable.BitmapDrawable
import android.os.Bundle
import android.view.View
import android.widget.ImageView
import androidx.appcompat.app.AlertDialog
import androidx.appcompat.app.AppCompatActivity
import androidx.core.graphics.scale
import com.example.tictactoe.databinding.ActivitySecondBinding


class SecondActivity : AppCompatActivity() {

    enum class Turn{
        CROSS,
        CIRCLE
    }

    private var firstTurn = Turn.CROSS;
    private var currentTurn = Turn.CROSS;

    private var player1 = Player();
    private var player2 = Player();
    private var victoryCircle: Boolean = false;
    private var victoryCross: Boolean = false;
    private var turn = 1;

    private var gameGrid = mutableListOf<ImageView>()

    private lateinit var binding: ActivitySecondBinding

    private var blank : Bitmap? = null;

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivitySecondBinding.inflate(layoutInflater);
        setContentView(binding.root);
        player1 = intent.getSerializableExtra("Player1") as Player;
        player2 = intent.getSerializableExtra("Player2") as Player;

        binding.txtTurn.text = "Turn X : " + player1.player_name;
        initGameGrid();
    }

    private fun initGameGrid()
    {
        gameGrid.add(binding.cell1);
        gameGrid.add(binding.cell2);
        gameGrid.add(binding.cell3);
        gameGrid.add(binding.cell4);
        gameGrid.add(binding.cell5);
        gameGrid.add(binding.cell6);
        gameGrid.add(binding.cell7);
        gameGrid.add(binding.cell8);
        gameGrid.add(binding.cell9);
    }

    fun gridTapped(view: View) {
        blank = BitmapFactory.decodeResource(resources, R.drawable.blank).scale(32,32);

        if (view !is ImageView)
            return;
        addToGameGrid(view);

        if (turn > 4) {
            if (checkForVictory() && currentTurn == Turn.CROSS) {
                disableGrid()
                victoryCircle = true;
                binding.btnReset.isEnabled = true;
                player2.player_score++;
                result("Circles Win!")
            }
            if (checkForVictory() && currentTurn == Turn.CIRCLE) {
                disableGrid()
                victoryCross = true;
                binding.btnReset.isEnabled = true;
                player1.player_score++;
                result("Crosses Win!")
            }
            if (fullGameGrid()) {
                disableGrid()
                binding.btnReset.isEnabled = true;
                result("Draw");
            }
        }
        turn++;
    }

    private fun disableGrid() {
        for (ImageView in gameGrid)
        {
            ImageView.isClickable = false;
        }
    }

    private fun checkForVictory(): Boolean {

        var bitmap1 : Bitmap =(binding.cell1.drawable as BitmapDrawable).bitmap;
        var bitmap2 : Bitmap =(binding.cell2.drawable as BitmapDrawable).bitmap;
        var bitmap3 : Bitmap =(binding.cell3.drawable as BitmapDrawable).bitmap;
        var bitmap4 : Bitmap =(binding.cell4.drawable as BitmapDrawable).bitmap;
        var bitmap5 : Bitmap =(binding.cell5.drawable as BitmapDrawable).bitmap;
        var bitmap6 : Bitmap =(binding.cell6.drawable as BitmapDrawable).bitmap;
        var bitmap7 : Bitmap =(binding.cell7.drawable as BitmapDrawable).bitmap;
        var bitmap8 : Bitmap =(binding.cell8.drawable as BitmapDrawable).bitmap;
        var bitmap9 : Bitmap =(binding.cell9.drawable as BitmapDrawable).bitmap;

        //Horizontal Victory
        if (bitmap1 == bitmap2 && bitmap2 == bitmap3) {
            if(!bitmap1.sameAs(blank) && !bitmap2.sameAs(blank) && !bitmap3.sameAs(blank))
                return true;
        }
        if (bitmap4 == bitmap5 && bitmap5 == bitmap6) {
            if(!bitmap4.sameAs(blank) && !bitmap5.sameAs(blank) && !bitmap6.sameAs(blank))
                return true;
        }

        if (bitmap7 == bitmap8 && bitmap8 == bitmap9) {
            if (!bitmap7.sameAs(blank) && !bitmap8.sameAs(blank) && !bitmap9.sameAs(blank))
                return true;
        }
        //Vertical Victory
        if (bitmap1 == bitmap4 && bitmap4 == bitmap7) {
            if(!bitmap1.sameAs(blank) && !bitmap4.sameAs(blank) && !bitmap7.sameAs(blank))
                return true;
        }
        if (bitmap2 == bitmap5 && bitmap5 == bitmap8) {
            if(!bitmap2.sameAs(blank) && !bitmap5.sameAs(blank) && !bitmap8.sameAs(blank))
                return true;
        }
        if (bitmap3 == bitmap6 && bitmap6 == bitmap9) {
            if(!bitmap3.sameAs(blank) && !bitmap6.sameAs(blank) && !bitmap9.sameAs(blank))
                return true;
        }
        //Diagonal Victory
        if (bitmap1 == bitmap5 && bitmap5 == bitmap9) {
            if(!bitmap1.sameAs(blank) && !bitmap5.sameAs(blank) && !bitmap9.sameAs(blank))
                return true;
        }
        if (bitmap3 == bitmap5 && bitmap5 == bitmap7) {
            if(!bitmap3.sameAs(blank) && !bitmap5.sameAs(blank) && !bitmap7.sameAs(blank))
                return true
        }
        return false
    }

    private fun result(title: String) {
        val message : String = "\nJoueur 1: ${player1.player_name} a un score de ${player1.player_score} avec les X" +
                                "\n\n Joueur 2: ${player2.player_name} a un score de ${player2.player_score} avec les O"
        AlertDialog.Builder(this)
            .setTitle(title)
            .setMessage(message)
            .setPositiveButton("OK")
            {
                _,_ ->
            }
            .show();
    }

    fun resetGameGrid(view: View) {
        for (ImageView in gameGrid)
        {
            ImageView.setImageResource(R.drawable.blank);
            ImageView.isClickable = true;
        }

        if (victoryCross)
            firstTurn = Turn.CROSS;
        else if(victoryCircle)
            firstTurn = Turn.CIRCLE;
        else {
            if (currentTurn == Turn.CROSS)
                firstTurn = Turn.CROSS;
            else if (currentTurn == Turn.CIRCLE)
                firstTurn = Turn.CIRCLE;
        }
        currentTurn = firstTurn;
        setTurnLabel();
        binding.btnReset.isEnabled = false;
        victoryCircle = false;
        victoryCross = false;
        turn = 1;
    }

    private fun fullGameGrid(): Boolean {
        for (ImageView in gameGrid)
        {
            var bm : Bitmap = (ImageView.drawable as BitmapDrawable).bitmap;
            if (bm.sameAs(blank))
                return false;
        }
        return true;
    }

    private fun addToGameGrid(imageview: ImageView) {

        var bm : Bitmap = (imageview.drawable as BitmapDrawable).bitmap;

        if(!bm.sameAs(blank))
            return;
        if (currentTurn == Turn.CROSS)
        {
            imageview.setImageResource(R.drawable.cross);
            currentTurn = Turn.CIRCLE;
        }
        else if (currentTurn == Turn.CIRCLE)
        {
            imageview.setImageResource(R.drawable.circle);
            currentTurn = Turn.CROSS;
        }
        setTurnLabel();
    }

    private fun setTurnLabel() {
        var turnText = "";
        if (currentTurn == Turn.CROSS)
            turnText = "Turn X : " + player1.player_name;
        else if (currentTurn == Turn.CIRCLE)
            turnText = "Turn O : " + player2.player_name;

        binding.txtTurn.text = turnText;
    }

    fun changePlayers()
    {
        val intent = Intent(this, MainActivity::class.java);
        startActivity(intent);
        finish();
    }
}