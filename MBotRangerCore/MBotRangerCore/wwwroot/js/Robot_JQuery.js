//var time = new Date().getTime();
//$(document.body).bind("mousemove keypress", function (e) {
//    time = new Date().getTime();
//});

//function refresh() {
//    if ((new Date().getTime() - time) >= 300000)
//        window.location.reload(true);
//    else
//        setTimeout(refresh, 2000);
//}

//setTimeout(refresh, 2000);




//Button control options with Jquery

$('#forwardBtn').on("mousedown", function () {
    RobotBtnOptions("1");
}).on("mouseup", function () {
    RobotBtnOptions("5");
});

$('#leftBtn').on("mousedown", function () {
    RobotBtnOptions("3");
}).on("mouseup", function () {
    RobotBtnOptions("5");
});
$('#rightBtn').on("mousedown", function () {
    RobotBtnOptions("4");
}).on("mouseup", function () {
    RobotBtnOptions("5");
});

$('#backBtn').on("mousedown", function () {
    RobotBtnOptions("2");
}).on("mouseup", function () {
    RobotBtnOptions("5");
});

$('#stopBtn').click(function () {
    RobotBtnOptions("5");
});


function RobotBtnOptions(_option) {
    var url = "/Robot/MoveRobotOption";
    
    $.post(url, { option: _option }, function (data) {
     //   $("#distance").html(data);
    });
}

setInterval(RobotDistance, 1000);


function RobotDistance() {
    $("#distance").load("/Robot/Index/ #distance");
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
    var idleTime = 0;
$(document).ready(function () {
    //Increment the idle time counter every minute.
    var idleInterval = setInterval(timerIncrement, 5000); // 5 seconds

    //Zero the idle timer on mouse movement.
    $(this).mousemove(function (e) {
        idleTime = 0;
    });
    $(this).keypress(function (e) {
        idleTime = 0;
    });
});

function timerIncrement() {
        idleTime = idleTime + 1;
    if (idleTime > 1) { // 2 minutes
        window.location.reload();
    }
}
*/
/*
var inactivityTime = function () {
    var t;
    window.onload = resetTimer;
    // DOM Events
    document.onmousemove = resetTimer;
    document.onkeypress = resetTimer;

    function logout() {
        alert("You are now logged out.")
        //location.href = 'logout.php'
    }

    function resetTimer() {
        clearTimeout(t);
        t = setTimeout(logout, 3000)
        // 1000 milisec = 1 sec
    }
};
*/

function idleLogout() {
    var timeIdle;
    var warnTime;
    window.onload = resetTimer;
    window.onmousemove = resetTimer;
    window.onmousedown = resetTimer; 
    window.onclick = resetTimer;     
    window.onscroll = resetTimer;    
    window.onkeypress = resetTimer;

    function logout() {
        //confirmation(); 
        var loggedOutEmail = document.getElementById("loggedUser").value;
        //location.href = '/Account/Logout?loggedOutEmail=' + loggedOutEmail;
       
        window.location = '/Account/Logout?loggedOutEmail=' + loggedOutEmail;
        
       // window.location = '/Account/Logout';
    }
    function warn() {
    /*  var seconds = 4;
        setInterval(function () {
            document.getElementById("logoutWarn").innerHTML = "You will be logged out in " + seconds + " seconds";
            seconds--;
            if (seconds < 0) {
                document.getElementById("logoutWarn").innerHTML = "";
                return;
            }
            return;
        }, 1000);*/

       document.getElementById("logoutWarn").innerHTML = "You will be logged out in 2 seconds";
    }

    function resetTimer() {
        document.getElementById("logoutWarn").innerHTML = "";
        clearTimeout(timeIdle);
        clearTimeout(warnTime);
        warnTime = setTimeout(warn, 3000);
        timeIdle = setTimeout(logout, 11117000);  // time is in milliseconds
    }
}
idleLogout();


$('#logInIN').click(function () {
    var timerID = document.getElementById("timerID").value;
    var msg = timerID;
    if (msg && msg.length > 0)
        alert(msg);
});

function confirmation() {
    var answer = confirm("You are about to be logged out, would you like to stay");
    if (answer) {
        window.location = '/Robot/Index';
    }
    else {
        window.location = '/Robot/Index';
    }
}

var xx = "ffw";
window.onbeforeunload = function dd()
{
   // xx = xx + xx;
     document.getElementById("unload").innerHTML = xx;

    
};



   
