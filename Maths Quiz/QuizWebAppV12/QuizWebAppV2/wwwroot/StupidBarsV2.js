let topBorder = document.getElementById("TopBorder");
let rightBorder = document.getElementById("RightBorder");
let bottomBorder = document.getElementById("BottomBorder");
let leftBorder = document.getElementById("LeftBorder");

let timeForQuizV2 = 10 * document.getElementById('NumberOfQuestionsForTimer').value;
let eachBorderTime = timeForQuizV2 / 4;

function BorderStuff(time)
{
    time = time + 1;
    if (time >= eachBorderTime * 3)
    {
        percentage = 100 - (((timeForQuizV2 - time) / eachBorderTime) * 100);
        updateBorderStyle(topBorder, percentage, "width");
        rightBorder.style.setProperty("height", "100%");
        bottomBorder.style.setProperty("width", "100%");
        leftBorder.style.setProperty("height", "100%");
        
    }
    else if (time >= eachBorderTime * 2)
    {
        percentage = 100 - (((timeForQuizV2 - time - eachBorderTime) / eachBorderTime) * 100);
        topBorder.style.setProperty("width", "0%");
        updateBorderStyle(rightBorder, percentage, "height");
        bottomBorder.style.setProperty("width", "100%");
        leftBorder.style.setProperty("height", "100%");
    }
    else if (time >= eachBorderTime)
    {
        percentage = Math.max(0, 100 - (((timeForQuizV2 - time - (eachBorderTime*2)) / eachBorderTime) * 100));
        updateBorderStyle(bottomBorder, percentage, "width");
        leftBorder.style.setProperty("height", "100%");
    }
    else
    {
        percentage = Math.max(0, 100 - (((timeForQuizV2 - time - (eachBorderTime * 3)) / eachBorderTime) * 100));
        bottomBorder.style.setProperty("width", "0%");
        updateBorderStyle(leftBorder, percentage, "height");
    }
}

function updateBorderStyle(borderElement, newSize, property) {
    borderElement.style.setProperty(property, newSize + "%");
    //if (property === 'width') {
    //    borderElement.style.transition = "width 1s ease";
    //} else {
    //    borderElement.style.transition = "height 1s ease";
    //}
}
