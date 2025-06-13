const mongoose = require('mongoose');
const Schema = mongoose.Schema;
var uniqueValidator = require('mongoose-unique-validator');
const ProduitSchema = new Schema({
  code: { type: String, required: true, unique: true },
  nom: { type: String, required: true },
  prix: { type: Schema.Types.Decimal128, required: true },
  quantite: {
    type: Number,
    default: 0
  },
  image: String,
  estEnStock: {
    type: Boolean,
    default: true
  }
});
ProduitSchema.plugin(uniqueValidator, {
  message: 'Le code du produit doit être unique'
});
const Produit = mongoose.model('Produit', ProduitSchema);
module.exports = Produit;
