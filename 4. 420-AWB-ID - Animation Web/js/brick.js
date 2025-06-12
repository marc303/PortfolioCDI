const BRICK_WIDTH = BRICKS_IMG.width / 10;
const BRICK_HEIGHT = BRICKS_IMG.height;

//CREATE THE BRICKS
const brick = {
  row: LEVEL * ROW_COEF,
  column: 10,
  width: BRICK_WIDTH,
  height: BRICK_HEIGHT,
  offsetLeft: 24,
  offsetTop: 24
};

//BRICKS COLOR ARRAY
let colors = [
  ['yellow', 0],
  ['brown', 0],
  ['red', 0],
  ['magenta', 0],
  ['cyan', 0],
  ['orange', 0],
  ['silver', 0],
  ['green', 0],
  ['blue', 0],
  ['black', 0]
];

let bricks = [];

//CREATE THE BRICKS AND THEIR PROPERTIES
function createBricks() {
  for (let r = 0; r < brick.row; r++) {
    bricks[r] = [];
    for (let c = 0; c < brick.column; c++) {
      let brickX;
      let brickY;
      let randomColor;
      do {
        randomColor = Math.floor(Math.random() * 10);
      } while (colors[randomColor][1] === LEVEL * ROW_COEF);

      let color = colors[randomColor][0];
      let hitPoints = assignBrickHP(color);

      brickX = c * (brick.width + brick.offsetLeft) + brick.offsetLeft + 6;
      brickY = r * (brick.height + brick.offsetTop) + brick.offsetTop + 6;

      bricks[r][c] = {
        x: brickX,
        y: brickY,
        color: randomColor,
        hp: hitPoints,
        status: true
      };
      colors[randomColor][1]++;
    }
  }
}

//ASSIGN HP TO BRICK
function assignBrickHP(color) {
  switch (color) {
    case 'silver':
      return 2;
    case 'black':
      return 3;
    default:
      return 1;
  }
}

//DRAW THE BRICKS
function drawBricks() {
  for (let r = 0; r < brick.row; r++) {
    for (let c = 0; c < brick.column; c++) {
      let b = bricks[r][c];
      if (b.status) {
        ctx_game.drawImage(
          BRICKS_IMG,
          BRICK_WIDTH * b.color,
          0,
          BRICK_WIDTH,
          BRICK_HEIGHT,
          b.x,
          b.y,
          brick.width,
          brick.height
        );
      }
    }
  }
}

//RESET COLORS
function resetColors() {
  for (let i = 0; i < colors.length; i++) {
    colors[i][1] = 0;
  }
}
