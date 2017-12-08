
var popCanvas = document.getElementById('popupCanvas');
var popContext = popCanvas.getContext('2d');
var captureByModal = document.getElementById("captureM");



///*******Webcamstart copied

//This file is for streaming from the webcam.
var localstream;

// Display containers
var video = document.getElementById('videoM');

// Buttons
var webcamStart = document.getElementById("webcamM");
var webcamStop = document.getElementById("stopM");

// Get the webcam and start it
webcamStart.addEventListener("click", function () {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // We can add `{ audio: true }` argument to access the audio
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            video.src = window.URL.createObjectURL(stream);
            localstream = stream;
            video.play();
        });
    }
});

// Stop the Webcam
webcamStop.addEventListener("click", function () {
    vidOff();
});



function vidOff() {
    video.pause();
    video.src = "";
    localstream.getTracks()[0].stop();
};


/////*********Webcam start end



captureByModal.addEventListener("click", function () {
    modal.style.display = "block";
    popContext.drawImage(video, 0, 0, 500, 330);
});





//Modals
var modal = document.getElementById('myModal');
var savePop = document.getElementById("popupSave");
var cancelPop = document.getElementById("popupCancel");
var prev = document.getElementById("previous");
var next = document.getElementById("next");
var controlButtons = document.getElementById("controlButtons");


prev.onclick = function () {
    controlButtons.style.display = "none";
};

next.onclick = function () {
    controlButtons.style.display = "block";
};

savePop.onclick = function () {
};

cancelPop.onclick = function () {
    modal.style.display = "none";
};

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
};

