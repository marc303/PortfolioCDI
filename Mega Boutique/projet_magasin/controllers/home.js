const Produit = require('../models/Produit');
module.exports = async (req, res) => {
  const produits = await Produit.find({ estEnStock: true });
  res.render('index', {
    produits
  });
};
