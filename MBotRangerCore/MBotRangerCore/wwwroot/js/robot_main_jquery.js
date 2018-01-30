//Button control options with Jquery
$('#forwardBtn').click(function () {
    RobotBtnOptions("1");   
});
$('#leftBtn').click(function () {
    RobotBtnOptions("3");
});
$('#rightBtn').click(function () {
    RobotBtnOptions("4");
});

$('#backBtn').click(function () {
    RobotBtnOptions("2");
});

$('#stopBtn').click(function () {
    RobotBtnOptions("5");   
});

function RobotBtnOptions(_option) {
    var url = "/Robot/MoveRobotOption";
    $.post(url, { option: _option }, function (data) {
        $("#msg").html(data);
    });
}

//WASD keys as arrow Options with Jquery
document.onkeydown = function (e) {
    if (e.keyCode === 65)        // Key 'A' to move left
        KeyOptions("3"); 
    else if (e.keyCode === 87)  // Key 'W' to move forward
        KeyOptions("1");
    else if (e.keyCode === 68)  //Key 'D' to move right
        KeyOptions("4");
    else if (e.keyCode === 83) //Key 'S' to move backward
        KeyOptions("2");   
};

/*
    When button is kept pressed(keydown), it only sends value once to contoller.
    Variable tempString is used to check if there is a change in the
    keydown/up calls and it make sure only excute KeyOptions function
    once until a change.
*/
var tempString = "";
function KeyOptions(_option) {
    if (tempString != _option) {
        tempString = _option;
        var url = "/Robot/MoveRobotArrowsOption";
        $.post(url, { str: _option }, function (data) {
            $("#msg2").html(data);
        });
    }    
}


document.onkeyup = function (e) {
    if (e.keyCode === 65 || e.keyCode === 68 || e.keyCode === 87 || e.keyCode === 83) 
        KeyOptions("5", 5); //Stop     
};

/*
// change the seconds into Hour:Minute:Seconds Format
// timeHours = parseInt(timerLogOut / 3600);
var tempTimerLog = timerLogOut;
var timeHours, timeMinutes, timeSeconds, timeAll;
timeHours = Math.floor(tempTimerLog / 3600);
tempTimerLog %= 3600;
timeMinutes = Math.floor(tempTimerLog / 60);
timeSeconds = tempTimerLog % 60;
var warnSecondsw2 = timeHours + " " + timeMinutes + " " + timeSeconds;
*/
var temptime = document.getElementById("SecondsWait").innerHTML;
var timerLogOut = temptime * 1000;
if (timerLogOut > 700000)
    var intialWarn = 15000;
else
    var intialWarn = 1000;
var warnSeconds = (timerLogOut - intialWarn) / 1000;

//Inactivity logging out and and take away Robot access from first user

//var timerLogOut = document.getElementById("timerLog").innerHTML;
//var intialWarn = 5000;
//var warnSeconds = (timerLogOut - intialWarn) / 1000;
var interSeconds = warnSeconds;
var logOutMsg;


// The following function log out the user automatically with two conditions;
// 1. Used the robot page for maximum time
// 2. Another user logs in and is waiting to access the robot page
function autoLogout() {
    var timeIdle;
    var warnTime;
    if (timerLogOut > 700000) {
        window.onload = resetTimer;
        window.onmousemove = resetTimer;
        window.onmousedown = resetTimer;
        window.onclick = resetTimer;
        window.onscroll = resetTimer;
        window.onkeypress = resetTimer;
       // logOutMsg = "You have been away for " + intialWarn / 1000 + " seconds and you will be log out in ";
        logOutMsg = "You used the maximum time, your session will end in ";
    }
    else
    {
        resetTimer();
        logOutMsg = "Someone requested to access, You will be log out in ";
    }
    

    function logout() {
        var x;
        var loggedOutEmail = document.getElementById("loggedUser").value;       
        window.location = '/Account/Logout?loggedOutEmail=' + loggedOutEmail;
    }
    var varForSetInterval; 
    function warn() {        
        varForSetInterval = setInterval(function () {            
            document.getElementById("logoutWarn").innerHTML = logOutMsg + interSeconds + " seconds";
          //  document.getElementById("GuestWaitTime").innerHTML = interSeconds + " seconds";
            //$("#logoutWarn").clone().appendTo("#logoutWarn");
           
            interSeconds--;
            if (interSeconds == 0)
            {
                document.getElementById("logoutWarn").innerHTML = "Bye";
                clearInterval(varForSetInterval);
            }
        }, 1000);
      //  document.getElementById("logoutWarn").innerHTML = logOutMsg + warnSeconds + " seconds";
        
    }
    

    function resetTimer() {
        document.getElementById("logoutWarn").innerHTML = "";
        clearInterval(varForSetInterval);
        interSeconds = warnSeconds;
        clearTimeout(timeIdle);
        clearTimeout(warnTime);
        warnTime = setTimeout(warn, intialWarn);
        timeIdle = setTimeout(logout, timerLogOut);  // time is in milliseconds
    }
}
autoLogout();



setInterval(function () {
    $("#waitingListTable").load("/Home/About #waitingListTable");
}, 5000);

/*
setInterval(function () {
    $("#SecondsWait").load("/Robot/Index #SecondsWait");  
}, 1000);
*/
/*
setInterval(function () {

    $("#logoutWarn").load("/Robot/Index #logoutWarn");
}, 1000);*/




/*
setInterval(getMyData, 5000);

function getMyData() {
    $.post("/Robot/Index", { submit: "5" });

}  */






///Bottom to be deleted


var xx = "ffw";
window.onbeforeunload = function dd() {
    // xx = xx + xx;
    document.getElementById("unload").innerHTML = xx;


};



   
