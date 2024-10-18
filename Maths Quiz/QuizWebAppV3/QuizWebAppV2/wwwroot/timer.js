let timeForQuiz = 10 * document.getElementById('NumberOfQuestionsForTimer').value;
const timerElement = document.getElementById('timer');


const interval = setInterval(updateTimer, 1000);

function updateTimer() {
    timerElement.innerHTML = timeForQuiz;
    timeForQuiz--;

    if (timeForQuiz < 10) {
        timerElement.setAttribute('class', 'errorMessage timer')
    }

    if (timeForQuiz < 0) {
        clearInterval(interval);
        window.location.href = "/Index?time=OutOfTime"
    }
}