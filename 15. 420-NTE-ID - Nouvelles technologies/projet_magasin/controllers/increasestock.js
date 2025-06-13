const Produit = require('../models/Produit');
module.exports = async (req, res) => {
  try {
    await Produit.findOneAndUpdate(
      { code: req.body.code },
      { $inc: { quantite: req.body.quantite } }
    ).orFail();
    res.redirect('/afficher');
  } catch (error) {
    let message = 'Le produit avec le code ' + req.body.code + " n'existe pas.";
    req.flash('message', message);
    return res.redirect('/recevoir');
  }
};
