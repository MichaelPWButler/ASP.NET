let timeForQuiz = sessionStorage.getItem("remainingSeconds") || 10 * document.getElementById('NumberOfQuestionsForTimer').value 
const timerElement = document.getElementById('timer');

const interval = setInterval(updateTimer, 1000);

let topBorder = document.getElementById("TopBorder");
let rightBorder = document.getElementById("RightBorder");
let bottomBorder =  document.getElementById("BottomBorder");
let leftBorder = document.getElementById("LeftBorder");

let topBorderWidth = topBorder.offsetWidth;
let rightBorderHeight = rightBorder.offsetHeight;
let bottomBorderWidth = bottomBorder.offsetWidth;
let leftBorderHeight = leftBorder.offsetHeight;

let totalLength = topBorderWidth + rightBorderHeight + bottomBorderWidth + leftBorderHeight;
let WidthInterval = totalLength / timeForQuiz;

function updateBorderStyle(borderElement, newSize, property) {
    newSize = Math.max(0, newSize);
    borderElement.style.setProperty(property, newSize + "px");
}

function updateTimer() {
    timerElement.innerHTML = timeForQuiz;
    timeForQuiz--;
    sessionStorage.setItem("remainingSeconds", timeForQuiz);

    if (timeForQuiz >= 0) {
        if (topBorderWidth > 0) {
            updateBorderStyle(topBorder, topBorderWidth - WidthInterval, 'width'); 
        } else if (rightBorderHeight > 0) {
            updateBorderStyle(rightBorder, rightBorderHeight - WidthInterval, 'height');
        } else if (bottomBorderWidth > 0) {
            updateBorderStyle(bottomBorder, bottomBorderWidth - WidthInterval, 'width');
        } else {
            updateBorderStyle(leftBorder, leftBorderHeight - WidthInterval, 'height');
        }
    }

    if (timeForQuiz < 10) {
        timerElement.setAttribute('class', 'errorMessage timer')
    }

    if (timeForQuiz < 0) {
        sessionStorage.removeItem("remainingSeconds"); 
        clearInterval(interval);

        window.location.href = "/Index?time=OutOfTime"
    }

    bottomBorderWidth = bottomBorder.offsetWidth;
    topBorderWidth = topBorder.offsetWidth;
    rightBorderHeight = rightBorder.offsetHeight;
    leftBorderHeight = leftBorder.offsetHeight;

    totalLength = topBorderWidth + rightBorderHeight + bottomBorderWidth + leftBorderHeight;
    WidthInterval = totalLength / timeForQuiz;

}