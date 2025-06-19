let currentLevel = 0;
let isGameStarted = false;
let isLevelDone = false;
let LIFE = 3; //PLAYER HAS 3 LIVES
let SCORE = 0;
let SCORE_UNIT = 10;
let GAME_OVER = false;
const FRAME_LENGTH = 1 / 60;

//SHOW SPLASH SCREEN
function splashScreen() {
  if (!isGameStarted) {
    ctx_game.drawImage(SPLASH_SCREEN, 0, 0);
    requestAnimationFrame(splashScreen);
  }
}

//LOADING BAR ANIMATION
let bar_progress = anime({
  targets: loadBar,
  value: 100,
  easing: 'linear',
  duration: 4000,
  autoplay: false,
  complete: function () {
    // loadDisplay('none');
    showHideElement(loadBar, 'none');
    showHideElement(loadTxt, 'none');
    gameLoop();
  }
});

//OBTAIN IMAGE LEVEL SOURCE
function levelSource(level) {
  switch (level) {
    case 1:
      return BG_LVL1;
    case 2:
      return BG_LVL2;
    case 3:
      return BG_LVL3;
    default:
      return BG_LVL1;
  }
}

//SHOW LOADING SCREEN
function loadingScreen() {
  ctx_game.drawImage(currentLevel, 0, 0);
  showHideElement(loadBar, 'block');
  showHideElement(loadTxt, 'block');
  bar_progress.play();
}
//DRAW FUNCTION
function draw() {
  drawPaddle();

  drawBall();

  drawBricks();

  showGameStats();
}

//UPDATE GAME FUNCTION
function update() {
  timeUpdate();

  moveBall();

  movePaddle();

  ballWallCollision();

  ballPaddleCollision();

  ballBrickCollision();

  if (LIFE <= 0) {
    gameOver();
  }
  if (!isLevelDone) {
    levelUp();
  }
}
//GAME LOOP
function gameLoop() {
  //ClEAR THE CANVAS
  ctx_game.drawImage(currentLevel, 0, 0);

  draw();

  update();

  if (!GAME_OVER && !isLevelDone) {
    requestAnimationFrame(gameLoop);
  }
}

//SHOW GAME DETAILS
function showGameStats() {
  details.style.display = 'block';
  $('#score').html(SCORE);
  switch (LIFE) {
    case 3:
      $('#lifebar').attr('class', 'full_lifebar');
      break;
    case 2:
      $('#lifebar').attr('class', 'two_lifebar');
      break;
    case 1:
      $('#lifebar').attr('class', 'one_lifebar');
      break;
    case 0:
      $('#lifebar').attr('class', 'empty_lifebar');
      break;
    default:
      $('#lifebar').attr('class', 'full_lifebar');
      break;
  }
}

function timeUpdate() {
  let currentFrameTimeStamp = performance.now();
  timeBetweenFrames = (currentFrameTimeStamp - lastFrameTimeStamp) / 1000; //temps en seconde
  lastFrameTimeStamp = currentFrameTimeStamp;
}

//GAME OVER
function gameOver() {
  GAME_OVER = true;
  showGameStats();
  showLoseScreen();
}

//LEVEL UP
function levelUp() {
  isLevelDone = true;
  for (let r = 0; r < brick.row; r++) {
    for (let c = 0; c < brick.column; c++) {
      isLevelDone = isLevelDone && !bricks[r][c].status;
    }
  }
  if (isLevelDone) {
    if (LEVEL >= MAX_LEVEL) {
      showWinningScreen();
      showHideElement(btnStart, 'block');
      GAME_OVER = true;
      return;
    }
    ball.speed += 50;
    resetPaddle();
    resetBall();
    showWinningScreen();
    LEVEL++;
    ROW_COEF = 2;
    setTimeout(nextLevel, 10000);
  }
}

//LAUNCH NEXT LEVEL
function nextLevel() {
  // btn_animation.pause();
  // winning_pulse.pause();
  resetBoard();
  loadingScreen();
  createBricks();
}

//RESET GAMES STATUSES
function resetBoard() {
  if (isLevelDone) {
    winning_pulse.pause();
    showHideElement(imgVictory, 'none');
    showHideElement(lvlText, 'none');
    showHideElement(stateText, 'none');
    isLevelDone = false;
  }
  if (GAME_OVER) {
    showHideElement(imgLaugh, 'none');
    showHideElement(gameOver_fr, 'none');
    showHideElement(gameOver_en, 'none');
    GAME_OVER = false;
    SCORE = 0;
    LIFE = 3;
    LEVEL = 1;
    ROW_COEF = 3;
  }
  currentLevel = levelSource(LEVEL);
  brick.row = LEVEL * ROW_COEF;
  resetColors();
}

//BUTTON ACTION FOR NEW GAME
btnStart.addEventListener('click', function () {
  isGameStarted = true;
  showHideElement(btnStart, 'none');
  nextLevel();
});

// PAUSE/UNPAUSE GAME
document.addEventListener('keypress', pauseGame, false);

function pauseGame(e) {
  if (e.keyCode == 32) {
    ballLoose = !ballLoose;
    if (isBallLaunched) paddleLoose = !paddleLoose;
    else {
      isBallLaunched = !isBallLaunched;
    }
  }

  //CHEAT CODE
  if (e.key == 'w') {
    for (let r = 0; r < brick.row; r++) {
      for (let c = 0; c < brick.column; c++) {
        let b = bricks[r][c];
        b.status = false;
      }
    }
  }
}
$(document).ready(() => {
  splash_animation.play();
  splashScreen();
});
