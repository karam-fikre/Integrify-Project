//The time is in milliseconds
//var warnAfterThisTime = (2 * 60 * 1000); // 2 Minutes for Idle time
//var warnAfterThisTime = (20 * 1000); // 20 seconds for DEMO
var warnAfterThisTime = (30 * 60 * 1000); // 30 Minutes for DEMO

var maxInactiveTime = warnAfterThisTime + (10 * 1000); 
var warnInMinutes = warnAfterThisTime / (60 * 1000);
var remainingTime = (maxInactiveTime - warnAfterThisTime) / 1000; //Change milliseconds to seconds
var intervalRemainingTime = remainingTime;

//When the user is in active for maxInactiveTime, they will be loggout automatically. 
//if one of the window.on methods called we reset the time.
function inactiveLogout() {
    var timeInactive;
    var warnTime;
    //If one of the following window methods called reset timer and give the user the max time again
    window.onload = resetTimer;
    window.onmousemove = resetTimer;
    window.onmousedown = resetTimer;
    window.onclick = resetTimer;
    window.onscroll = resetTimer;
    window.onkeypress = resetTimer;

    function logout() {
        var loggedOutEmail = document.getElementById("loggedUser").value;
        window.location = '/Account/Logout?loggedOutEmail=' + loggedOutEmail;
    }
    var var_interval;
    function warn() {
        var_interval = setInterval(function () {
            document.getElementById("logoutWarnIDLE").innerHTML = "You have been inactive for " + warnInMinutes + " minutes and you will be logged out in " + intervalRemainingTime + " seconds";

            intervalRemainingTime--;
            if (intervalRemainingTime < 1) {
                document.getElementById("logoutWarnIDLE").innerHTML = "Logging out...";
                clearInterval(var_interval);
            }
        }, 1000);
       

    }


    function resetTimer() {
        document.getElementById("logoutWarnIDLE").innerHTML = "";
        clearInterval(var_interval);
        intervalRemainingTime = remainingTime;
        clearTimeout(timeInactive);
        clearTimeout(warnTime);
        warnTime = setTimeout(warn, warnAfterThisTime);
        timeInactive = setTimeout(logout, maxInactiveTime);  
    }
}
inactiveLogout();