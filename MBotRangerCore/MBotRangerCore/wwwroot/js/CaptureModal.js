var popCanvas = document.getElementById('popupCanvas');
var popContext = popCanvas.getContext('2d');
var captureByModal = document.getElementById("captureModal");



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
}


/////*********Webcam start end



captureByModal.addEventListener("click", function () {
    modal.style.display = "block";
    popContext.drawImage(video, 0, 0, 500, 330);
});






//Modals
var modal = document.getElementById('myModalRobot');
var savePop = document.getElementById("popupSave");
var cancelPop = document.getElementById("popupCancel");

cancelPop.onclick = function () {
    modal.style.display = "none";
};
savePop.onclick = function () {
    modal.style.display = "none";
};

window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
};


//Below not needed
//var changeToRobot = document.getElementById("robot_");
var changeToWebcam = document.getElementById("webcam_");
var controlButtons = document.getElementById("controlButtons");
var webcamButtons = document.getElementById("webcamButtons");
webcamButtons.style.display = "none";
var counter = false;

/*
changeToRobot.onclick = function () {
    controlButtons.style.display = "block";
    webcamButtons.style.display = "none";
};
*/

changeToWebcam.onclick = function () {
    //controlButtons.style.display = "none";
    // webcamButtons.style.display = "block";
    if (counter === false) {
        controlButtons.style.display = "none";
        webcamButtons.style.display = "block";
        changeToWebcam.innerHTML = "Change to Robot Options";
        counter = true;
    }
    else {
        controlButtons.style.display = "block";
        webcamButtons.style.display = "none";
        changeToWebcam.innerHTML = "Change to Webcam Options";
        counter = false;
    }

};

