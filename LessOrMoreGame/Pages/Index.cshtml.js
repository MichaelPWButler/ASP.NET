'use strict';

const control_Country1 = document.getElementById('Country1'),
    control_Country2 = document.getElementById('Country2'),
    control_CorrectOverlay = document.getElementById('overlay-correct'),
    control_WrongOverlay = document.getElementById('overlay-wrong');

control_Country1.addEventListener('click', () => _checkCard(control_Country1.dataset.id, control_Country2.dataset.id, "left"));
control_Country2.addEventListener('click', () => _checkCard(control_Country2.dataset.id, control_Country1.dataset.id, "right"));

async function _checkCard(idSelected, OtherId, cardSelected) {
    const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

    const response = await fetch('/Index?handler=CheckCard', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json', 'Accept': 'application/json', 'RequestVerificationToken': token
        },
        body: JSON.stringify(
        {
            CountrySelectedID: idSelected,
            OtherCountryId: OtherId
        }) 
    });

    const result = await response.json();
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