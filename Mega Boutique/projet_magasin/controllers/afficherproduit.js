const Produit = require('../models/Produit');
module.exports = async (req, res) => {
  const produits = await Produit.find({});
  res.render('afficherproduit', {
    produits
  });
};
