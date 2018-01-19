var waiterWaitingTime = document.getElementById("youWait").innerHTML;
var initSecond = waiterWaitingTime;
if (waiterWaitingTime != null) {
    setInterval(function () {
        document.getElementById("youWait").innerHTML = initSecond;

        initSecond--;
        if (initSecond  < 1) {
            document.getElementById("youWait").innerHTML = "Refresh to access the page";
        }
    }, 1000);
}