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
var timerLogOut = document.getElementById("timerLog").innerHTML;
var intialWarn = 3000;
var warnSeconds = (timerLogOut - intialWarn) / 1000;
var interSeconds = warnSeconds;
var logOutMsg;



function idleLogout() {
    var timeIdle;
    var warnTime;
    if (timerLogOut > 30000) {
        window.onload = resetTimer;
        window.onmousemove = resetTimer;
        window.onmousedown = resetTimer;
        window.onclick = resetTimer;
        window.onscroll = resetTimer;
        window.onkeypress = resetTimer;
        logOutMsg = "You have been away for " + intialWarn/1000 + " seconds and you will be log out in ";
    }
    else
    {
        resetTimer();
        logOutMsg = "Someone requested to access, You will be log out in ";
    }
    

    function logout() {
        var x;
        var loggedOutEmail = document.getElementById("loggedUser").value;       
        window.location = '/Account/LoseAccess?loggedOutEmail=' + loggedOutEmail;
    }
    var her; 
    function warn() {
        
        her = setInterval(function () {
            
            document.getElementById("logoutWarn").innerHTML = "You will be logged out in " + interSeconds + " seconds";
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
    $("#waitingListTable").load("/Robot/Index #waitingListTable");
}, 5000);

setInterval(getMyData, 5000);

function getMyData() {
    $.post("/Robot/Index", { submit: "5" });
   // document.getElementById("json").innerHTML = m;
    // Using $.getJSON for simplicity, but try to use $.ajax or $.post
    /*  $.ajax('/Robot/MyAction', function (response) {
          var items = response.d.items;
          document.getElementById("innerDivLogged").innerHTML = items;
          document.getElementById("innerDivLogged").contentWindow.location.reload(true);
          // Iterate through the items here and add the items to your web page
      });*/
}  


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



   
