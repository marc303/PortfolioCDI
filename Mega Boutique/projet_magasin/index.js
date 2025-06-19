const express = require('express');
const path = require('path');
const app = express();
const ejs = require('ejs');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const Produit = require('./models/Produit');
const fileUpload = require('express-fileupload');
const expressSession = require('express-session');
const connectflash = require('connect-flash');

const validationCreateMiddleware = require('./middleware/validationCreateMiddleware');
const diminutionStockMiddleware = require('./middleware/diminutionStockMiddleware');
const estEnStockMiddleware = require('./middleware/estEnStockMiddleware');

const homeController = require('./controllers/home');
const creerproduitController = require('./controllers/creerproduit');
const storeproduitController = require('./controllers/storeproduit');
const recevoirstockController = require('./controllers/recevoirstock');
const increasestockController = require('./controllers/increasestock');
const envoyerstockController = require('./controllers/envoyerstock');
const decreasestockController = require('./controllers/decreasestock');
const afficherproduitController = require('./controllers/afficherproduit');
const retirerproduitController = require('./controllers/retirerproduit');
const removefromlistController = require('./controllers/removefromlist');
const itemsearchController = require('./controllers/itemsearch');

mongoose.set('strictQuery', false);
mongoose.connect('mongodb://127.0.0.1/megaboutique');

app.set('view engine', 'ejs');
app.use(express.static('public'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(fileUpload());
app.use('/produits/store', validationCreateMiddleware);
app.use(
  expressSession({
    secret: 'green arrow',
    resave: true,
    saveUninitialized: true
  })
);
app.use(connectflash());
app.listen(5000, () => {
  console.log('Application ecoute sur le port 5000');
});

app.get('/', homeController);
app.get('/produits/new', creerproduitController);
app.get('/recevoir', recevoirstockController);
app.get('/envoyer', envoyerstockController);
app.get('/afficher', afficherproduitController);
app.get('/retirer', retirerproduitController);
app.get('/find', itemsearchController);

app.post('/produits/store', storeproduitController);
app.post('/produits/increasestock', increasestockController);
app.post(
  '/produits/decreasestock',
  diminutionStockMiddleware,
  decreasestockController
);
app.post(
  '/produits/removefromlist',
  estEnStockMiddleware,
  removefromlistController
);
