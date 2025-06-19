const PADDLE_WIDTH = PADDLE_IMG.width;
const PADDLE_HEIGHT = PADDLE_IMG.height;
const PADDLE_MARGIN_BOTTOM = 30;
let leftPressed = false;
let rightPressed = false;
let ballLoose = false;
let paddleLoose = false;

//CREATE THE PADDLE
const paddle = {
  x: (cvs_game.width - PADDLE_WIDTH) / 2,
  y: cvs_game.height - PADDLE_HEIGHT - PADDLE_MARGIN_BOTTOM,
  width: PADDLE_WIDTH,
  height: PADDLE_HEIGHT,
  dx: 1600
};

//DRAW PADDLE
function drawPaddle() {
  ctx_game.drawImage(
    PADDLE_IMG,
    0,
    0,
    paddle.width,
    paddle.height,
    paddle.x,
    paddle.y,
    paddle.width,
    paddle.height
  );
}

//CONTROL THE PADDLE
document.addEventListener('keydown', keyDownHandler, false);
document.addEventListener('keyup', keyUpHandler, false);
cvs_game.addEventListener('mousemove', mouseMoveHandler, false);

function keyDownHandler(e) {
  //LEFT ARROW OR "A" KEY
  if (e.keyCode == 37 || e.keyCode == 65) {
    leftPressed = true;
  }
  //RIGHT ARROW OR "D" KEY
  else if (e.keyCode == 39 || e.keyCode == 68) {
    rightPressed = true;
  }
}

function keyUpHandler(e) {
  //LEFT ARROW OR "A" KEY
  if (e.keyCode == 37 || e.keyCode == 65) {
    leftPressed = false;
  }
  //RIGHT ARROW OR "D" KEY
  else if (e.keyCode == 39 || e.keyCode == 68) {
    rightPressed = false;
  }
}

function mouseMoveHandler(e) {
  let relativeX = e.clientX - cvs_game.offsetLeft;
  if (paddleLoose) {
    if (relativeX - paddle.width >= 0 && relativeX < cvs_game.width) {
      paddle.x = relativeX - paddle.width;
      if (!ballLoose && ball.y === paddle.y - BALL_HEIGHT)
        ball.x = paddle.x + (paddle.width / 2 - ball.radius); //center ball above paddle when pause
    }
  }
}

//MOVE THE PADDLE
function movePaddle() {
  if (!isBallLaunched) {
    paddleLoose = true;
  }
  if (paddleLoose) {
    if (rightPressed && paddle.x + paddle.width < cvs_game.width) {
      paddle.x += paddle.dx * timeBetweenFrames;
      if (!ballLoose) ball.x += paddle.dx * timeBetweenFrames;
    } else if (leftPressed && paddle.x > 0) {
      paddle.x -= paddle.dx * timeBetweenFrames;
      if (!ballLoose) ball.x -= paddle.dx * timeBetweenFrames;
    }
  }
}

//RESET THE PADDLE
function resetPaddle() {
  paddleLoose = false;
  paddle.x = (cvs_game.width - PADDLE_WIDTH) / 2;
}
