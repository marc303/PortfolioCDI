module.exports = (req, res) => {
  res.render('envoyerstock', {
    message: req.flash('message')
  });
};
