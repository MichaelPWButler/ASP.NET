'use strict';

const control_Country1 = document.getElementById('Country1'),
    control_Country2 = document.getElementById('Country2'),
    control_CorrectOverlay = document.getElementById('overlay-correct'),
    control_WrongOverlay = document.getElementById('overlay-wrong');

control_Country1.addEventListener('click', _checkCard);
control_Country2.addEventListener('click', _checkCard);

async function _checkCard() {
    const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

        const response = await fetch('/Index?handler=CheckCard', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json', 'Accept': 'application/json', 'RequestVerificationToken': token
            },
            body: JSON.stringify(12) 
        });

    const isValid = await response.json();
    _openOverlay(isValid)
}

function _openOverlay(isValid) {
    if (isValid) {
        control_CorrectOverlay.classList.add('active');
        setTimeout(() => {
            control_CorrectOverlay.classList.remove('active');
        }, 1000);
    } else {
        control_WrongOverlay.classList.add('active');
        setTimeout(() => {
            control_WrongOverlay.classList.remove('active');
        }, 1000);
    }
}
