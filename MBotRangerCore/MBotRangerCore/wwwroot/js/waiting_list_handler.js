var guest_WaitTime = document.getElementById("guestWaitTime").innerHTML;
var initialWaitSeconds = guest_WaitTime;
if (guest_WaitTime !== null) {
    setInterval(function () {

        var timeInHours = Math.floor(initialWaitSeconds / 3600);
        var timeInMinutes = Math.floor((initialWaitSeconds % 3600) / 60);
        var timeInSeconds = Math.floor(initialWaitSeconds % 60);
    
        if (timeInHours < 10) timeInHours = "0" + timeInHours;
        if (timeInMinutes < 10) timeInMinutes = "0" + timeInMinutes;
        if (timeInSeconds < 10) timeInSeconds = "0" + timeInSeconds;
        var timeAll = timeInHours + " : " + timeInMinutes + " : " + timeInSeconds;

        document.getElementById("guestWaitTime").innerHTML = timeAll;
       

        initialWaitSeconds--;
        if (initialWaitSeconds  < 1) {
            document.getElementById("guestWaitTime").innerHTML = "Refreshing";
            window.location = '/Robot/Index';
        }
    }, 1000);
}
