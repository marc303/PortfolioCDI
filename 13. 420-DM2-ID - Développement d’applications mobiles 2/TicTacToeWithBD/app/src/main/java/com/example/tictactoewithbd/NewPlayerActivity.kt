package com.example.tictactoewithbd

import android.app.Activity
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.TextUtils
import android.widget.EditText
import com.example.tictactoewithbd.databinding.ActivityNewPlayerBinding
import com.example.tictactoewithbd.tictactoedb.Player

class NewPlayerActivity : AppCompatActivity() {

    private lateinit var binding : ActivityNewPlayerBinding
    private lateinit var editPlayerView: EditText

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityNewPlayerBinding.inflate(layoutInflater)
        setContentView(binding.root)
        editPlayerView = binding.editPlayer

        val button = binding.buttonSave
        button.setOnClickListener{
            val replyIntent = Intent()
            if(TextUtils.isEmpty(editPlayerView.text)){
                setResult(Activity.RESULT_CANCELED, replyIntent)
            }else{
                val newPlayer = editPlayerView.text.toString()
                replyIntent.putExtra(EXTRA_REPLY, newPlayer)
                setResult(Activity.RESULT_OK, replyIntent)
            }
            finish()
        }
    }

    companion object{
        const val EXTRA_REPLY = "com.example.android.wordlistsql.REPLY"
    }
}