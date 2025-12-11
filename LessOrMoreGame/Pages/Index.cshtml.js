'use strict';

const control_Country1 = document.getElementById('Country1'),
    control_Overlay = document.getElementById('overlay');

control_Country1.addEventListener('click', function () {
    overlay.classList.add('active');

    setTimeout(() => {
        overlay.classList.remove('active');
    }, 1000);
});