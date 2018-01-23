//Ourmaster branch edit

     // $.post("/Robot/RobotArrows", { id: 3 });
         //   jQuery('[id$="myHiddenField"]').val(param);

                //$.ajax({
                //    url: '@Url.Action("RobotArrows","Robot")',
                //    data: JSON.stringify({ "id": 3 }),
                //    type: 'POST',
                //    contentType: 'application/json; charset=utf-8',
                //    success: function (data) {
                //        // return values 
                //        console.log("Success!" + data.id);
                //    },
                //    error: function () { console.log('error!!'); }
                //});
//Original

var idleTime = 1800000;
var intialWarn2 = 59000;
var warnSeconds2 = (idleTime - intialWarn2) / 1000;
var interSeconds2 = warnSeconds2;

function idleLogout2() {
    var timeIdle;
    var warnTime;
    window.onload = resetTimer;
    window.onmousemove = resetTimer;
    window.onmousedown = resetTimer;
    window.onclick = resetTimer;
    window.onscroll = resetTimer;
    window.onkeypress = resetTimer;

    function logout() {
        var x;
        var loggedOutEmail = document.getElementById("loggedUser").value;
        window.location = '/Account/Logout?loggedOutEmail=' + loggedOutEmail;
    }
    var her;
    function warn() {
        her = setInterval(function () {
            document.getElementById("logoutWarnIDLE").innerHTML = "You have been Idle for " +intialWarn2/1000 + " seconds and you will be logged out in " + interSeconds2 + " seconds";

            interSeconds2--;
            if (interSeconds2 == 0) {
                document.getElementById("logoutWarnIDLE").innerHTML = "Bye";
                clearInterval(her);
            }
        }, 1000);
        //  document.getElementById("logoutWarn").innerHTML = logOutMsg + warnSeconds + " seconds";

    }


    function resetTimer() {
        document.getElementById("logoutWarnIDLE").innerHTML = "";
        clearInterval(her);
        interSeconds2 = warnSeconds2;
        clearTimeout(timeIdle);
        clearTimeout(warnTime);
        warnTime = setTimeout(warn, intialWarn2);
        timeIdle = setTimeout(logout, idleTime);  // time is in milliseconds
    }
}
idleLogout2();
