let SB = 25; //SHADOW BLUR
let SB_SPEED = -5;

//START BUTTON ANIMATON
let btn_animation = anime({
  targets: btnStart,
  scale: [
    { value: 1 },
    { value: 1.15 },
    { value: 1.3 },
    { value: 1.15 },
    { value: 1 }
  ],
  autoplay: false,
  duration: 3000,
  easing: 'linear',
  loop: true
});

//SPLASH SCREEN ANIMATON
let splash_animation = anime({
  targets: cvs_game,
  translateX: '-175%',
  duration: 5000,
  easing: 'linear',
  direction: 'reverse',
  autoplay: false,
  complete: function () {
    // btnDisplay('block');
    showHideElement(btnStart, 'block');
    btn_animation.play();
  }
});

//LOSING TEXT SCALING EFFECT
let losing_pulse = anime({
  targets: [gameOver_en, gameOver_fr],
  duration: 4000,
  scale: [
    { value: 1 },
    { value: 0.75 },
    { value: 0.5 },
    { value: 0.75 },
    { value: 1 }
  ],
  easing: 'linear',
  autoplay: false,
  loop: true
});

//LOSING TEXT TRANSITIOM EFFECT
let losing_transition = anime({
  targets: [gameOver_en, gameOver_fr],
  translateY: 800,
  duration: 1500,
  easing: 'linear',
  direction: 'reverse',
  autoplay: false,
  complete: function () {
    losing_pulse.play();
    showHideElement(btnStart, 'block');
  }
});

//WINNING TEXT SCALING EFFECT
let winning_pulse = anime({
  targets: [imgVictory, lvlText, stateText],
  duration: 8000,
  scale: [
    { value: 0.5 },
    { value: 0.75 },
    { value: 1 },
    { value: 0.75 },
    { value: 0.5 }
  ],
  easing: 'linear',
  autoplay: false,
  loop: true
});

//WINNING TEXT TRANSITION EFFECT
let winning_transition = anime({
  targets: [imgVictory, lvlText, stateText],
  translateY: 800,
  duration: 2000,
  easing: 'linear',
  direction: 'reverse',
  autoplay: false,
  complete: function () {
    winning_pulse.play();
  }
});

//LOSING SCREEN

//SHOW LOSING SCREEN ELEMENT
function showLoseScreen() {
  showHideElement(imgLaugh, 'block');
  showHideElement(gameOver_fr, 'block');
  showHideElement(gameOver_en, 'block');
  animateRedEyes();
  losing_transition.play();
}

//RED EYES ANIMATION
function animateRedEyes() {
  let reqAnimFrame =
    window.mozRequestAnimationFrame ||
    window.webkitRequestAnimationFrame ||
    window.msRequestAnimationFrame ||
    window.oRequestAnimationFrame;

  reqAnimFrame(animateRedEyes);
  if (SB >= 100 || SB <= 25) {
    SB_SPEED = -SB_SPEED;
  }
  SB += SB_SPEED * FRAME_LENGTH;
  drawRedEyes();
}

//DRAW THE RED EYES
function drawRedEyes() {
  ctx_game.drawImage(currentLevel, 0, 0);
  ctx_game.save();
  ctx_game.rotate((-5 * Math.PI) / 180);
  ctx_game.shadowBlur = SB;
  ctx_game.shadowColor = '#ff0000';
  ctx_game.drawImage(
    RED_EYES,
    0,
    0,
    RED_EYES.width,
    RED_EYES.height,
    cvs_game.width / 10,
    150,
    RED_EYES.width * 0.75,
    RED_EYES.height * 0.75
  );
  ctx_game.restore();
}

//WINNING SCREEN

//SHOW WINNING SCREEN ELEMENT
function showWinningScreen() {
  ctx_game.drawImage(currentLevel, 0, 0);
  $('#lvlNo').html(LEVEL);
  showHideElement(imgVictory, 'block');
  showHideElement(lvlText, 'block');
  showHideElement(stateText, 'block');
  winning_transition.play();
}
