let topBorderI = document.getElementById("TopBorder");
let rightBorderI = document.getElementById("RightBorder");
let bottomBorderI = document.getElementById("BottomBorder");
let leftBorderI = document.getElementById("LeftBorder");

if (sessionStorage.getItem("remainingSeconds") == null) {
    topBorderI.style.setProperty("width", "100%");
    rightBorderI.style.setProperty("height", "100%");
    bottomBorderI.style.setProperty("width", "100%");
    leftBorderI.style.setProperty("height", "100%");
}