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



//Buttons
var forward_Button = document.getElementById('forwardBtn');
var left_Button = document.getElementById('leftBtn');
var right_Button = document.getElementById('rightBtn');
var back_Button = document.getElementById('backBtn');
var stop_Button = document.getElementById('stopBtn');

forward_Button.disabled = true;
left_Button.disabled = true;
right_Button.disabled = true;
back_Button.disabled = true;
stop_Button.disabled = true;


var popCanvas = document.getElementById('popupCanvas');
var popContext = popCanvas.getContext('2d');
var captureByModal = document.getElementById("captureModal");
captureByModal.disabled = true;

//This file is for streaming from the webcam.
var localstream;

// Display containers
var video = document.getElementById('videoM');
var imageContainer = document.getElementById('imageContainer');



// Buttons
var webcamStart = document.getElementById("webcamM");
var webcamStop = document.getElementById("stopM");
webcamStop.disabled = true;

// Get the webcam and start it
/*
webcamStart.addEventListener("click", function () {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // We can add `{ audio: true }` argument to access the audio
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            video.src = window.URL.createObjectURL(stream);
            localstream = stream;
            video.play();
        });
    }
});*/


webcamStart.addEventListener("click", function () {
  
    imageContainer.src = "http://192.168.1.104:8080/video";
    captureByModal.disabled = false;
    forward_Button.disabled = false;
    left_Button.disabled = false;
    right_Button.disabled = false;
    back_Button.disabled = false;
    stop_Button.disabled = false;
    webcamStop.disabled = false;
   // webcamStart.disabled = true;
            

});


// Stop the Webcam
webcamStop.addEventListener("click", function () {

    
    imageContainer.src = "../images/robotStart_gif.gif";
    captureByModal.disabled = true;
    forward_Button.disabled = true;
    left_Button.disabled = true;
    right_Button.disabled = true;
    back_Button.disabled = true;
    stop_Button.disabled = true;
    webcamStop.disabled = true;
    webcamStart.disabled = false;

  //  vidOff();

});

function vidOff() {
    video.pause();
    video.src = "";
    localstream.getTracks()[0].stop();
}

captureByModal.addEventListener("click", function () {
    modal.style.display = "block";
    //popContext.drawImage(video, 0, 0, 500, 330);
   popContext.drawImage(imageContainer, 0, 0, 500, 330);
});




//Modals
var modal = document.getElementById('myModalRobot');
var savePop = document.getElementById("popupSave");
var cancelPop = document.getElementById("popupCancel");

cancelPop.onclick = function () {
    modal.style.display = "none";
};


var image = popCanvas.toDataURL("image/png").replace("image/png", "image/octet-stream");  // here is the most important part because if you dont replace you will get a DOM 18 exception.

savePop.onclick = function () {
    modal.style.display = "none";
    var img = document.createElement("img");
    img.src = popCanvas.toDataURL();
    $.ajax({
        type: "POST",
        url: '../../Gallery/SaveSnapshot',
        dataType: 'text',
        data: { dataType: img.src },
        success: function (result) { alert(result); }
    });
};

window.onclick = function (event) {
    if (event.target === modal) 
        modal.style.display = "none";   
};


