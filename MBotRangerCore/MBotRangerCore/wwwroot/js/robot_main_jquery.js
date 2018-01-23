


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


//Arrow Options with Jquery

document.onkeydown = function (e) {
    if (e.keyCode === 37)       // left
        KeyOptions("3");
    else if (e.keyCode === 38)  // forward
        KeyOptions("1");
    else if (e.keyCode === 39) //right
        KeyOptions("4"); 
    else if (e.keyCode === 40) //back
        KeyOptions("2"); 
};

function KeyOptions(_option) {
    var url = "/Robot/MoveRobotArrowsOption";
    $.post(url, { str: _option }, function (data) {
        $("#msg2").html(data);
    });
}

document.onkeyup = function (e) {
    if (e.keyCode === 37 || e.keyCode === 38 || e.keyCode === 39 || e.keyCode === 40) 
    {
        KeyOptions("5"); //Stop
    }    
};





/*
//change the seconds into Hour:Minute:Seconds Format
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

function idleLogout() {
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
    var her; 
    function warn() {        
        her = setInterval(function () {            
            document.getElementById("logoutWarn").innerHTML = logOutMsg + interSeconds + " seconds";
          //  document.getElementById("GuestWaitTime").innerHTML = interSeconds + " seconds";
            //$("#logoutWarn").clone().appendTo("#logoutWarn");
           
            interSeconds--;
            if (interSeconds == 0)
            {
                document.getElementById("logoutWarn").innerHTML = "Bye";
                clearInterval(her);
            }
        }, 1000);
      //  document.getElementById("logoutWarn").innerHTML = logOutMsg + warnSeconds + " seconds";
        
    }
    

    function resetTimer() {
        document.getElementById("logoutWarn").innerHTML = "";
        clearInterval(her);
        interSeconds = warnSeconds;
        clearTimeout(timeIdle);
        clearTimeout(warnTime);
        warnTime = setTimeout(warn, intialWarn);
        timeIdle = setTimeout(logout, timerLogOut);  // time is in milliseconds
    }
}
idleLogout();



setInterval(function () {
    $("#waitingListTable").load("/Home/About #waitingListTable");
}, 5000);




/*
setInterval(getMyData, 5000);

function getMyData() {
    $.post("/Robot/Index", { submit: "5" });

}  */





$('button').click(function (e) {
    $('#someID').data("key", "newValue").trigger('changeData');
});

$('#someID').on('changeData', function (e) {
    alert('My Custom Event - Change Data Called! for ' + this.id);
});







var xx = "ffw";
window.onbeforeunload = function dd() {
    // xx = xx + xx;
    document.getElementById("unload").innerHTML = xx;


};



   
