const Produit = require('../models/Produit');
module.exports = async (req, res) => {
  await Produit.findOneAndUpdate(
    { code: req.body.code },
    { $inc: { quantite: req.body.quantite * -1 } }
  );
  res.redirect('/afficher');
};
