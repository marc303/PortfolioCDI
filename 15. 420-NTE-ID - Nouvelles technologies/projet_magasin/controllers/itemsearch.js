const Produit = require('../models/Produit');
module.exports = async (req, res) => {
  const produits = await Produit.find({
    nom: { $regex: req.query.search, $options: 'i' }
  });
  res.render('afficherproduit', {
    produits
  });
};
