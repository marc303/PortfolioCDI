const Produit = require('../models/Produit');
module.exports = async (req, res, next) => {
  try {
    let data = await Produit.findOne(
      { code: req.body.code },
      'estEnStock'
    ).orFail();
    let db_estEnStock = data.estEnStock;
    if (!db_estEnStock) {
      let message = 'Le produit a déjà été retiré de la liste';
      req.flash('message', message);
      return res.redirect('/retirer');
    } else next();
  } catch (error) {
    let message = 'Le produit avec le code ' + req.body.code + " n'existe pas.";
    req.flash('message', message);
    return res.redirect('/retirer');
  }
};
