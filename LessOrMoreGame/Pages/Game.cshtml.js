'use strict';

const control_Country1 = document.getElementById('Country1'),
    control_Country2 = document.getElementById('Country2'),
    control_CorrectOverlay = document.getElementById('overlay-correct'),
    control_Streak = document.getElementById('streak'),
    control_QuestionStat = document.getElementById('question'),
    control_WrongOverlay = document.getElementById('overlay-wrong'),
    control_QuestionText = document.getElementById('questionText');

let currentStreak = 0;

control_Country1.addEventListener('click', () => _checkCard(control_Country1.dataset.id, control_Country2.dataset.id, "left"));
control_Country2.addEventListener('click', () => _checkCard(control_Country2.dataset.id, control_Country1.dataset.id, "right"));

async function _checkCard(idSelected, OtherId, cardSelected) {
    const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

    const response = await fetch('/Game?handler=CheckCard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json', 'Accept': 'application/json', 'RequestVerificationToken': token
        },
        body: JSON.stringify(
        {
            CountrySelectedID: idSelected,
            OtherCountryId: OtherId,
            Stat: Number(control_QuestionStat.dataset.question)
        }) 
    });

    const result = await response.json();
    _UpdateText(result.isCorrect, result.newQuestion, result.newStat)
    _openOverlay(result.isCorrect)
    _updateCard(result.newCountry, cardSelected)
}

function _updateCard(newCard, replaced) {
    let cardElement = replaced === 'left' ? control_Country1 : control_Country2;

    cardElement.dataset.id = newCard.id;

    const img = cardElement.querySelector('img');
    img.src = newCard.FlagImgSrc;

    const title = cardElement.querySelector('h2.card-title');
    title.textContent = newCard.name;
}

function _openOverlay(isCorrect) {
    if (isCorrect) {
        control_CorrectOverlay.classList.add('active');
        setTimeout(() => {
            control_CorrectOverlay.classList.remove('active');
        }, 1000);
    }
    else {
        control_WrongOverlay.classList.add('active');
        setTimeout(() => {
            control_WrongOverlay.classList.remove('active');
        }, 1000);
    }
}

function _UpdateText(isCorrect, newQuestion, newStat) {
    if (isCorrect) {
        currentStreak++;
    }
    else {
        currentStreak = 0;
    } 
    control_Streak.textContent = "Your current streak is: " + currentStreak;
    control_QuestionText.textContent = newQuestion
    control_QuestionStat.dataset.question = newStat;
}