var popCanvas = document.getElementById('popupCanvas');
var popContext = popCanvas.getContext('2d');
var captureByModal = document.getElementById("captureModal");


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

var image = popCanvas.toDataURL("image/png").replace("image/png", "image/octet-stream");  

savePop.onclick = function () {
    modal.style.display = "none";
   // window.location.href = image; // 
//window.location = "/Gallery/Upload?file=" + image;
$.ajax({
    type: "POST",
    url: '../../Robot/SaveSnapshot',
    dataType: 'text',
    data: { dataType: image },
    success: function (result) { alert(result); }
});

}

window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
};


////Public private toggle
var private = document.getElementById("private");
var public = document.getElementById("public");
var isPublic = false;

private.onclick = function () {
    document.getElementById("public").style.backgroundColor = "black";
    document.getElementById("public").style.color = "#262222"; 
    document.getElementById("private").style.backgroundColor = "green"; 
    document.getElementById("private").style.color = "white";
    isPublic = false;
    PublicPrivate(isPublic);
};


public.onclick = function () {    
    document.getElementById("private").style.backgroundColor = "black"; 
    document.getElementById("private").style.color = "#262222"; 
    document.getElementById("public").style.backgroundColor = "green"; 
    document.getElementById("public").style.color = "white";
    isPublic = true;
    PublicPrivate(isPublic);

    //$("#divVideo").clone().appendTo("#divVideoGuest");

};

//function PublicPrivate(_isPublic) {

//    window.location = '/Robot/Index?isPublic=' + _isPublic;
//}

function PublicPrivate(_isPublic) {
    var url = "/Robot/Index";
    $.post(url, { submit: "8", isPublic: _isPublic });
  //  $("#ppp").load("/Robot/Index #privateDiv");
}

