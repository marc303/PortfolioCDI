package com.example.tictactoewithbd.tictactoedb

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.tictactoewithbd.R

class PlayerListAdapter : ListAdapter<Player, PlayerListAdapter.PlayerViewHolder>(PlayersComparator()) {

    private var onClickListener : OnClickListenerPlayer? = null
    var isCLickable : Boolean = true

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PlayerViewHolder {
        return PlayerViewHolder.create(parent)
    }

    override fun onBindViewHolder(holder: PlayerViewHolder, position: Int) {
        val current = getItem(position)
        holder.bind(current.name, current.score.toString())

        holder.itemView.setOnClickListener {
            if (!isCLickable)
                return@setOnClickListener;
            if (onClickListener != null) {
                onClickListener!!.onClick(position, current)
            }
        }
    }

    fun setOnClickListener(onClickListener: OnClickListenerPlayer) {
        this.onClickListener = onClickListener
    }

    interface OnClickListenerPlayer{
        fun onClick(position: Int, player: Player)
    }

    class PlayerViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val playerNameItemView: TextView = itemView.findViewById(R.id.tvPlayerName)
        private val playerScoreItemView: TextView = itemView.findViewById(R.id.tvPlayerScore)

        fun bind(name: String?, score: String?){
            playerNameItemView.text = name
            playerScoreItemView.text = score
        }

        companion object{
            fun create(parent: ViewGroup): PlayerViewHolder{
                val view: View = LayoutInflater.from(parent.context)
                    .inflate(R.layout.player_rv, parent, false)
                return PlayerViewHolder(view)
            }
        }
    }

    class PlayersComparator : DiffUtil.ItemCallback<Player>() {
        override fun areItemsTheSame(oldItem: Player, newItem: Player): Boolean {
            return oldItem === newItem
        }

        override fun areContentsTheSame(oldItem: Player, newItem: Player): Boolean {
            return oldItem.name == newItem.name
        }

    }
}