var img = document.getElementById("captureImage");

var button = document.querySelector("button");

var containerImg = document.getElementById("captureImage");

var canvas = document.createElement("canvas");

canvas.width = 500;
canvas.height = 330;

var ctx = canvas.getContext("2d");


button.addEventListener("click", function () {
    ctx.drawImage(containerImg, 100, 100)
    containerImg.src = canvas.toDataURL();
});


