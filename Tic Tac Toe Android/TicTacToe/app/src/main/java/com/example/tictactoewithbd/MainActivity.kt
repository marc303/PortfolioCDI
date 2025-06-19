package com.example.tictactoewithbd

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.activity.viewModels
import androidx.core.view.isVisible
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.tictactoewithbd.databinding.ActivityMainBinding
import com.example.tictactoewithbd.tictactoedb.Player
import com.example.tictactoewithbd.tictactoedb.PlayerListAdapter
import com.example.tictactoewithbd.tictactoedb.PlayerViewModel
import com.example.tictactoewithbd.tictactoedb.PlayerViewModelFactory
import com.example.tictactoewithbd.tictactoedb.TicTacToeApplication

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding;
    private var Player1: Player? = null
    private var Player2: Player? = null

    private val playerViewModel: PlayerViewModel by viewModels {
        PlayerViewModelFactory((application as TicTacToeApplication).player_repository)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val recyclerView = binding.recyclerViewPlayer
        val adapter = PlayerListAdapter()
        recyclerView.adapter = adapter
        recyclerView.layoutManager = LinearLayoutManager(this)

        playerViewModel.allPlayers.observe(this, Observer { players ->
            players?.let { adapter.submitList(it) }
        })

        binding.tvMessagePlayer.text = "Choisissez le Joueur 1"

        val getResult = registerForActivityResult(
            ActivityResultContracts.StartActivityForResult()
        ){result ->
            if (result.resultCode == Activity.RESULT_OK)
            {
                result.data?.getStringExtra(NewPlayerActivity.EXTRA_REPLY)?.let {
                    val player = Player(it, 0)
                    playerViewModel.insert(player)
                }
            }
            else{
                Toast.makeText(
                    applicationContext,
                    R.string.empty_not_saved,
                    Toast.LENGTH_LONG).show()
            }
        }

        adapter.setOnClickListener(object : PlayerListAdapter.OnClickListenerPlayer{
            override fun onClick(position: Int, player: Player) {
                //val intent = Intent(this@MainActivity, ImageActivity::class.java)
                if(Player1 == null)
                {
                    Player1 = player
                    binding.tvMessagePlayer.text = "Choisissez le Joueur 2"
                }
                else if(Player2 == null && !Player1!!.equals(player))
                {
                    Player2 = player
                    binding.tvMessagePlayer.isVisible = false
                }
                else{
                    Toast.makeText(this@MainActivity, "Ce joueur a déjà été choisi", Toast.LENGTH_LONG).show()
                }
//                intent.putExtra("player1", Player1)
                if(Player1 != null && Player2 != null){
                    binding.tvPlayer1.text = "Joueur 1 : " + Player1!!.name
                    binding.tvPlayer2.text = "Joueur 2 : " + Player2!!.name
                    binding.btnChoisirImage.isEnabled = true
                    adapter.isCLickable = false
                }
            }
        })

        val fab = binding.fab
        fab.setOnClickListener{
            val intent = Intent(this@MainActivity, NewPlayerActivity::class.java)
            getResult.launch(intent)
        }

    }

    fun choisirImages(view: View) {
        val intent = Intent(this@MainActivity, ImageActivity::class.java)
        intent.putExtra("player1", Player1)
        intent.putExtra("player2", Player2)
        startActivity(intent)
    }
}