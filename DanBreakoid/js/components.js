//SELECT CANVAS ELEMENT
const cvs_game = $('#gameCanvas')[0];
const ctx_game = cvs_game.getContext('2d');

//SELECT DOM ELEMENTS
const btnStart = $('#pressStart')[0];
const loadBar = $('#loadingBar')[0];
const loadTxt = $('#txtLoading')[0];
const details = $('.details')[0];
const imgLaugh = $('#laugh')[0];
const gameOver_fr = $('#gameover_french')[0];
const gameOver_en = $('#gameover_english')[0];
const imgVictory = $('#victory')[0];
const lvlText = $('#lvlText')[0];
const stateText = $('#stateText')[0];

//GLOBAL CONSTANTS AND VARIABLES
let LEVEL = 1;
let ROW_COEF = 3; //ROW MULTIPLICATOR
const MAX_LEVEL = 3;
let lastFrameTimeStamp = 0;
let timeBetweenFrames = 0;
let currentLifeFrame = 0;

//SHOW/HIDE DOM ELEMENT
function showHideElement(elem, displayStyle) {
  elem.style.display = displayStyle;
}
