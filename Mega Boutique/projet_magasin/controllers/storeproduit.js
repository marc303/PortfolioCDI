const Produit = require('../models/Produit');
const path = require('path');
module.exports = async (req, res) => {
  let image = req.files.image;
  image.mv(path.resolve(__dirname, '../public/assets/img', image.name));
  try {
    var newProduit = new Produit({
      code: req.body.code,
      nom: req.body.nom,
      prix: req.body.prix,
      quantite: 0,
      image: '/assets/img/' + image.name
    });
    await Produit.create(newProduit);
    res.redirect('/afficher');
  } catch (error) {
    if (error.name === 'ValidationError') {
      const erreurs = Object.keys(error.errors).map(
        (key) => error.errors[key].message
      );
      req.flash('erreurs', erreurs);
      return res.redirect('/produits/new');
    }
  }
};
