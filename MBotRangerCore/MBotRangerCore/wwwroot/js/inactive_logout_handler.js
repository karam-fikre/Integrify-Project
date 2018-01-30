//The time is in milliseconds
var maxInactiveTime = 100000; //TODO change to large number in normal condition
var warnAfterThisTime = 10000;
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
            document.getElementById("logoutWarnIDLE").innerHTML = "You have been inactive for " + warnAfterThisTime / 1000 + " seconds and you will be logged out in " + intervalRemainingTime + " seconds";

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