
//Display container
var img = document.getElementById("mobileVideo");
var containerImg = document.getElementById("captureImage");
var canvas = document.getElementById('canvas');
var ctx = canvas.getContext("2d");

//Buttons
var stopStreaming = document.getElementById("stopStreaming");
var capture = document.getElementById("capture");
var play = document.getElementById("playVideo");

//Capture the image through streaming
capture.addEventListener("click", function () {
    ctx.drawImage(img, 0, 0 ,500,330)
    canvas.src = canvas.toDataURL('image/jpeg', 1.0);
});

//Stop streaming of the mobile camera
stopStreaming.addEventListener("click", function () {
    img.src = "//:0";
});

//Play video of the mobile camera
play.addEventListener("click", function () {
    img.src = "http://192.168.1.130:8080/video";
});