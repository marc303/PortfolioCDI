const Produit = require('../models/Produit');
module.exports = async (req, res, next) => {
  try {
    let data = await Produit.findOne(
      { code: req.body.code },
      'quantite'
    ).orFail();
    let db_quantite = data.quantite;
    if (db_quantite - req.body.quantite < 0) {
      let message = 'Stock insuffisant pour la quantité à expédier';
      req.flash('message', message);
      return res.redirect('/envoyer');
    } else next();
  } catch (error) {
    let message = 'Le produit avec le code ' + req.body.code + " n'existe pas.";
    req.flash('message', message);
    return res.redirect('/envoyer');
  }
};
