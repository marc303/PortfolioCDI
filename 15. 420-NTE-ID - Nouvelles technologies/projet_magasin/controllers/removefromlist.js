const Produit = require('../models/Produit');
module.exports = async (req, res) => {
  await Produit.findOneAndUpdate(
    { code: req.body.code },
    { estEnStock: false }
  );
  res.redirect('/afficher');
};
