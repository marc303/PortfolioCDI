const tokenColors = ["blue", "red", "yellow"];
let mysteryOrder = new Array();
let hidden = true;
let victory;
let attempt;

let attemptElement;
let correctElement;
let gameElement;
let mysteryElement;
let playableElement;
let verifyElement;

function init() {
  let main = document.getElementById("main");
  let template = document.getElementById("init");

  main.innerHTML = template.innerHTML;

  attemptElement = document.getElementById("attempt");
  correctElement = document.getElementById("correct");
  gameElement = document.getElementById("game");
  mysteryElement = document.getElementById("mystery");
  playableElement = document.getElementById("playable");
  verifyElement = document.getElementById("verify");
}

function newGame() {
  //(Re)initialise la variable victory
  victory = false;

  //(Re)initialise le nombre de tentatives
  attempt = 5;

  //Reinitialise les elements lors d'une nouvelle partie
  if (!hidden) {
    init();
    hidden = true;
  }

  //Afffiche les elements caches
  if (hidden) {
    gameElement.removeAttribute("hidden");
    verifyElement.removeAttribute("hidden");
    hidden = false;
  }

  attemptElement.innerHTML = attempt;

  //Appel de la methode pour la creation de l'ordre aleatoire
  randomizeTokens();
}

function randomizeTokens() {
  let count = mysteryElement.children.length;

  //Creation de l'ordre aleatoire
  for (let i = 0; i < count; i++) {
    let random = Math.floor(Math.random() * count);
    let token = tokenColors[random];
    mysteryOrder[i] = token;
  }
}

function changeColor(element) {
  //Couleur de l'element cliqué
  let color = element.getAttribute("value");

  //Change la couleur selon la couleur actuelle
  switch (color) {
    case "blue":
      changeClassAttribute(element, tokenColors[1]);
      break;
    case "red":
      changeClassAttribute(element, tokenColors[2]);
      break;
    case "yellow":
      changeClassAttribute(element, tokenColors[0]);
      break;
  }
}

function verifyOrder() {
  //Initialise le nombre de jeton(s) correct(s)
  let correct = 0;

  //Obtient la liste des jetons cliquables
  let playables = playableElement.children;

  let index = 0;

  //Boucle vérifiant le nombre de jeton(s) correct(s)
  for (const token of playables) {
    let color = token.getAttribute("value");

    if (color === mysteryOrder[index]) {
      correct++;
    }
    index++;
  }
  //Methode validant si la tentative est gagnante ou perdante
  validateGame(correct);
}

function validateGame(correct) {
  //S'il reste des essais
  if (attempt > 0) {
    //Si le joueur a selectionne l'ordre gagnant
    if (correct === 3) {
      victory = true;
      endGame();
    }
    //Diminution du nombre de tentative(s) restante(s)
    attempt--;
  }
  //Si le joueur arrive à 0 tentative restante
  if (attempt === 0) {
    endGame();
  }

  //Affichage des tentative(s) restante(s) et du nombre de jeton(s) correct(s)
  correctElement.innerHTML = correct;
  attemptElement.innerHTML = attempt;
}

function endGame() {
  //Affiche l'ordre mystere et retire le point ? des jetons mysteres
  let mysteries = mysteryElement.children;
  let p = mysteryElement.getElementsByTagName("p");

  for (let i = 0; i < mysteries.length; i++) {
    changeClassAttribute(mysteries[i], mysteryOrder[i]);
    p[i].innerHTML = "";
  }

  //Desactive le clic sur les jetons jouables
  let playables = playableElement.children;
  for (const playable of playables) {
    playable.setAttribute("style", "pointer-events: none;");
  }

  //Desactive le bouton Verifier
  verifyElement.setAttribute("disabled", "");

  //Affichage un message de fin dans la zone jeu
  let finalMessage = gameElement.getElementsByTagName("h2")[0];
  if (victory) {
    finalMessage.innerHTML = "Vouz avez gagné!!!";
  } else {
    finalMessage.innerHTML = "Vous avez perdu!";
  }
}

function changeClassAttribute(element, color) {
  element.className = color + " circle";
  element.setAttribute("value", color);
}
