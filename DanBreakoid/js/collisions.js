//CHECK BALL COLLISION WITH BRICKS
function ballBrickCollision() {
  let directionChanged = false;
  for (let r = 0; r < brick.row; r++) {
    for (let c = 0; c < brick.column; c++) {
      let b = bricks[r][c];
      if (b.status) {
        if (
          ball.x + ball.width > b.x &&
          ball.x < b.x + brick.width &&
          ball.y + ball.height > b.y &&
          ball.y < b.y + brick.height
        ) {
          if (!directionChanged) {
            directionChanged = true;
            collisionDirection(b);
          }
          b.hp--;
          if (b.hp === 0) b.status = false;
          SCORE += SCORE_UNIT;
        }
      }
    }
  }
}

//CHECK BALL DIRECTION ON BRICK COLLISION AND
//CHANGE BALL DIRECTION ACCORDINGLY
function collisionDirection(hitBrick) {
  let hitFromLeft =
    ball.x + ball.width - ball.dx * timeBetweenFrames <= hitBrick.x;
  let hitFromRight =
    ball.x - ball.dx * timeBetweenFrames >= hitBrick.x + brick.width;
  let hitFromTop =
    ball.y + ball.height - ball.dy * timeBetweenFrames <= hitBrick.y;
  let hitFromBottom =
    ball.y - ball.dy * timeBetweenFrames >= hitBrick.y + brick.height;

  if (hitFromLeft != hitFromRight) {
    ball.dx = -ball.dx;
  }

  if (hitFromTop != hitFromBottom) {
    ball.dy = -ball.dy;
  }
}

//BALL AND WALL COLLISION DETECTION
function ballWallCollision() {
  if (ball.x < 0 && ball.dx <= 0) {
    ball.dx = -ball.dx;
  }
  if (ball.x + ball.radius * 2 > cvs_game.width && ball.dx >= 0) {
    ball.dx = -ball.dx;
  }
  if (ball.y < 0 && ball.dy <= 0) {
    ball.dy = -ball.dy;
  }
  if (ball.y + ball.radius > cvs_game.height) {
    LIFE--;
    resetPaddle();
    resetBall();
  }
}

//BALL AND PADDLE COLLISION
function ballPaddleCollision() {
  if (
    ball.y + ball.radius > paddle.y && //DOWN
    ball.y + ball.radius < paddle.y + paddle.height && //UP
    ball.x + ball.radius > paddle.x &&
    ball.x + ball.radius < paddle.x + paddle.width
  ) {
    //CHECK WHERE THE BALL HIT PADDLE
    let collidePoint = ball.x + ball.radius - (paddle.x + paddle.width / 2);
    //NORMALIZE THE VALUES
    collidePoint = collidePoint / (paddle.width / 2);
    //CALCULATE THE ANGLE OF THE BALL
    let angle = collidePoint * (Math.PI / 3);

    ball.dx = ball.speed * Math.sin(angle);
    ball.dy = -ball.speed * Math.cos(angle);
  }
}
