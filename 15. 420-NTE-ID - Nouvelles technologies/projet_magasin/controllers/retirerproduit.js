module.exports = (req, res) => {
  res.render('retirerproduit', {
    message: req.flash('message')
  });
};
