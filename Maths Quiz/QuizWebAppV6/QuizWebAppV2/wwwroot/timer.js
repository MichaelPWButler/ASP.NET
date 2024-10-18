let timeForQuiz = sessionStorage.getItem("remainingSeconds") || 10 * document.getElementById('NumberOfQuestionsForTimer').value 
const timerElement = document.getElementById('timer');

const interval = setInterval(updateTimer, 1000);


function updateTimer() {
    timerElement.innerHTML = timeForQuiz;
    timeForQuiz--;
    sessionStorage.setItem("remainingSeconds", timeForQuiz);
    

    if (timeForQuiz < 10) {
        timerElement.setAttribute('class', 'errorMessage timer')
    }

    if (timeForQuiz < 0) {
        sessionStorage.removeItem("remainingSeconds"); 
        clearInterval(interval);
        window.location.href = "/Index?time=OutOfTime"
    }
}