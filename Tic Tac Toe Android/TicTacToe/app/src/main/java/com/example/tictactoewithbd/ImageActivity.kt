package com.example.tictactoewithbd

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.activity.viewModels
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.tictactoewithbd.databinding.ActivityImageBinding
import com.example.tictactoewithbd.tictactoedb.Image
import com.example.tictactoewithbd.tictactoedb.ImageListAdapter
import com.example.tictactoewithbd.tictactoedb.ImageViewModel
import com.example.tictactoewithbd.tictactoedb.ImageViewModelFactory
import com.example.tictactoewithbd.tictactoedb.Player
import com.example.tictactoewithbd.tictactoedb.PlayerListAdapter
import com.example.tictactoewithbd.tictactoedb.TicTacToeApplication

class ImageActivity : AppCompatActivity() {

    private lateinit var binding: ActivityImageBinding

    private lateinit var Player1: Player
    private lateinit var Player2: Player
    private var Player1Image: Image? = null
    private var Player2Image: Image? = null

    private val imageViewModel: ImageViewModel by viewModels {
        ImageViewModelFactory((application as TicTacToeApplication).image_repository)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityImageBinding.inflate(layoutInflater)
        setContentView(binding.root)

        Player1 = intent.getSerializableExtra("player1") as Player
        Player2 = intent.getSerializableExtra("player2") as Player

        binding.tvMessageImage.text = Player1.name + ", choisissez une image"

        val recyclerView = binding.recyclerViewImage
        val adapter = ImageListAdapter()
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(this)

        imageViewModel.allImages.observe(this, Observer { images ->
            images?.let { adapter.submitList(it) }
        })

        adapter.setOnClickListener(object : ImageListAdapter.OnClickListenerImage{
            override fun onClick(position: Int, image: Image) {
                if(Player1Image == null)
                {
                    Player1Image = image
                    binding.tvMessageImage.text = Player2.name + ", choisissez une image"
                }
                else if(Player2Image == null && !Player1Image!!.equals(image) )
                {
                    Player2Image = image
                    binding.tvMessageImage.text = "Vous pouvez débuter la partie"
                }
                else{
                    Toast.makeText(this@ImageActivity, "Cette image a déjà été choisi", Toast.LENGTH_LONG).show()
                }

                if (Player1Image != null && Player2Image != null)
                {
                    binding.tvImagePlayer1.text = Player1.name + " (Joueur 1) :"
                    binding.ivPlayer1.setImageResource(Player1Image!!.path)
                    binding.tvImagePlayer2.text = Player2.name + " (Joueur 2) :"
                    binding.ivPlayer2.setImageResource(Player2Image!!.path)
                    binding.btnStart.isEnabled = true
                    adapter.isCLickable = false;
                }

            }
        })
    }

    fun startGame(view: View) {
        val intent = Intent(this@ImageActivity, GameActivity::class.java)
        intent.putExtra("player1", Player1)
        intent.putExtra("player2", Player2)
        intent.putExtra("player1Image", Player1Image)
        intent.putExtra("player2Image", Player2Image)
        startActivity(intent)
    }
}