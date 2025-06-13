module.exports = (req, res) => {
  res.render('creerproduit', {
    erreurs: req.flash('erreurs')
  });
};
