const BALL_WIDTH = BALL_IMG.width;
const BALL_HEIGHT = BALL_IMG.height;
const BALL_RADIUS = BALL_WIDTH / 2;
let isBallLaunched = false;

//CREATE THE BALL
const ball = {
  x: cvs_game.width / 2 - BALL_RADIUS,
  y: paddle.y - BALL_HEIGHT,
  width: BALL_WIDTH,
  height: BALL_HEIGHT,
  radius: BALL_RADIUS,
  speed: 600,
  dx: 600 * (Math.random() * 2 - 1),
  dy: -600
};

//DRAW THE BALL
function drawBall() {
  ctx_game.drawImage(
    BALL_IMG,
    0,
    0,
    BALL_WIDTH,
    BALL_HEIGHT,
    ball.x,
    ball.y,
    ball.width,
    ball.height
  );
}

//MOVE THE BALL
function moveBall() {
  if (ballLoose) {
    ball.x += ball.dx * timeBetweenFrames;
    ball.y += ball.dy * timeBetweenFrames;
  }
}

//RESET THE BALL
function resetBall() {
  ballLoose = false;
  isBallLaunched = false;
  ball.x = paddle.x + (paddle.width / 2 - BALL_RADIUS);
  ball.y = paddle.y - BALL_HEIGHT;
  ball.dx = ball.speed * (Math.random() * 2 - 1);
  ball.dy = -ball.speed;
}
