package com.example.tictactoewithbd.tictactoedb

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.tictactoewithbd.R

class ImageListAdapter : ListAdapter<Image, ImageListAdapter.ImageViewHolder>(ImagesComparator()) {

    private var onClickListener : OnClickListenerImage? = null
    var isCLickable : Boolean = true

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ImageViewHolder {
        return  ImageViewHolder.create(parent)
    }

    override fun onBindViewHolder(holder: ImageViewHolder, position: Int) {
        val current = getItem(position)
        holder.bind(current.name, current.path)

        holder.itemView.setOnClickListener {
            if (!isCLickable)
                return@setOnClickListener;
            if (onClickListener != null) {
                onClickListener!!.onClick(position, current)
            }
        }
    }

    fun setOnClickListener(onClickListener: OnClickListenerImage) {
        this.onClickListener = onClickListener
    }

    interface OnClickListenerImage{
        fun onClick(position: Int, image: Image)
    }

    class ImageViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val imageNameItemView: TextView = itemView.findViewById(R.id.tvImageName)
        private val imageItemView: ImageView = itemView.findViewById(R.id.ivImage)

        fun bind(name: String?, path: Int?){
            imageNameItemView.text = name
            if (path != null) {
                imageItemView.setImageResource(path)
            }
        }

        companion object{
            fun create(parent: ViewGroup): ImageViewHolder{
                val view: View = LayoutInflater.from(parent.context)
                    .inflate(R.layout.image_rv, parent, false)
                return ImageViewHolder(view)
            }
        }
    }

    class ImagesComparator : DiffUtil.ItemCallback<Image>() {
        override fun areItemsTheSame(oldItem: Image, newItem: Image): Boolean {
            return oldItem === newItem
        }

        override fun areContentsTheSame(oldItem: Image, newItem: Image): Boolean {
            return oldItem.name == newItem.name
        }

    }
}
