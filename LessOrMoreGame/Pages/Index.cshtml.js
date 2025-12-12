'use strict';

const control_Country1 = document.getElementById('Country1');
const control_Overlay = document.getElementById('overlay');

control_Country1.addEventListener('click', _checkCard);

async function _checkCard() {
    const cardId = 42; // replace with the actual integer you want to send
    const token = document.querySelector('meta[name="csrf-token"]').getAttribute('content');

    try {
        const response = await fetch('/Index?handler=CheckCard', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json', 'Accept': 'application/json', 'RequestVerificationToken': token
            },
            body: JSON.stringify({ cardId }) // wrap integer in object
        });

        if (!response.ok) {
            console.error('Server returned:', response.status);
            return; // stop if server returns error
        }

        const isValid = await response.json();

        if (isValid) {
            control_Overlay.classList.add('active');
            setTimeout(() => {
                control_Overlay.classList.remove('active');
            }, 1000);
        } else {
            console.warn('Card is not valid');
        }

    } catch (error) {
        console.error('Fetch error:', error);
    }
}
