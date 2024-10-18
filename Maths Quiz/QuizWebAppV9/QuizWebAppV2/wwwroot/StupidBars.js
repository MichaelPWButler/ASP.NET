let topBorder = document.getElementById("TopBorder");
let rightBorder = document.getElementById("RightBorder");
let bottomBorder = document.getElementById("BottomBorder");
let leftBorder = document.getElementById("LeftBorder");
function BorderUpdate(timeForQuiz)
{
    let topBorderWidth = setInitialStupidBorder("topBorder");
    let rightBorderHeight = setInitialStupidBorder("rightBorder");
    let bottomBorderWidth = setInitialStupidBorder("bottomBorder");
    let leftBorderHeight = setInitialStupidBorder("leftBorder");

    let totalLength = topBorderWidth + rightBorderHeight + bottomBorderWidth + leftBorderHeight;
    if (sessionStorage.getItem("WI") == null)
    {
        WidthInterval = totalLength / timeForQuiz;
        sessionStorage.setItem("WI", WidthInterval)
    }
    else
    {
        WidthInterval = sessionStorage.getItem("WI");
    }

    if (topBorderWidth > 0) {
        topBorderWidth = Math.max(0, topBorderWidth - WidthInterval);
        updateBorderStyle(topBorder, topBorderWidth, 'width');
    } else if (rightBorderHeight > 0) {
        rightBorderHeight = Math.max(0, rightBorderHeight - WidthInterval);
        updateBorderStyle(rightBorder, rightBorderHeight, 'height');
    } else if (bottomBorderWidth > 0) {
        bottomBorderWidth = Math.max(0, bottomBorderWidth - WidthInterval);
        updateBorderStyle(bottomBorder, bottomBorderWidth, 'width');
    } else {
        leftBorderHeight = Math.max(0, leftBorderHeight - WidthInterval);
        updateBorderStyle(leftBorder, leftBorderHeight, 'height');
    }

    sessionStorage.setItem("topBorder", topBorderWidth);
    sessionStorage.setItem("rightBorder", rightBorderHeight);
    sessionStorage.setItem("bottomBorder", bottomBorderWidth);
    sessionStorage.setItem("leftBorder", leftBorderHeight);
}


function updateBorderStyle(borderElement, newSize, property) {
    borderElement.style.setProperty(property, newSize + "px");
}


function setInitialStupidBorder(border) {
    if (sessionStorage.getItem(border) == null) {
        if (border == "topBorder" || border == "bottomBorder") {
            returnBorder = eval(border).offsetWidth;
            sessionStorage.setItem(border, returnBorder);
            return returnBorder;
        }
        else {
            returnBorder = eval(border).offsetHeight;
            sessionStorage.setItem(border, returnBorder);
            return returnBorder;
        }

    }
    else if (sessionStorage.getItem(border) == 0) {
        if (border == "topBorder" || border == "bottomBorder") {
            updateBorderStyle(eval(border), 0, 'width');
            returnBorder = parseFloat(sessionStorage.getItem(border));
            return returnBorder;
        }
        else {
            updateBorderStyle(eval(border), 0, 'height');
            returnBorder = parseFloat(sessionStorage.getItem(border));
            return returnBorder;
        }
    }
    else {
        returnBorder = parseFloat(sessionStorage.getItem(border));
    }
    return returnBorder;
}
