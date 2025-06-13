module.exports = (req, res, next) => {
  if (
    req.files == null ||
    req.body.code == null ||
    req.body.nom == null ||
    req.body.prix == null
  ) {
    return res.redirect('/produits/new');
  }
  next();
};
