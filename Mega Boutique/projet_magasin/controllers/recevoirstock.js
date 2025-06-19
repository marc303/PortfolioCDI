module.exports = (req, res) => {
  res.render('recevoirstock', {
    message: req.flash('message')
  });
};
