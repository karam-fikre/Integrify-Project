
var localstream;

// Display containers
var video = document.getElementById('video');
var canvas = document.getElementById('canvas');
var context = canvas.getContext('2d');

// Buttons
var webcamStart = document.getElementById("webcam");
var webcamStop = document.getElementById("stop");
var webcamCapture = document.getElementById("capture");

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

// Capture a photo from the webcamstream.
webcamCapture.addEventListener("click", function () {
    context.drawImage(video, 0, 0, 500, 330);
});

function vidOff() {
    video.pause();
    video.src = "";
    localstream.getTracks()[0].stop();
}





var clickHere = document.getElementById('clickhere');

clickHere.addEventListener("click", function () {
    location.href = '/Webcam/TestingAction?id=' + 6;
});