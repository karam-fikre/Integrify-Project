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
    if (tempString !== _option) {
        tempString = _option;
        var url = "/Robot/MoveRobotArrowsOption";
        $.post(url, { str: _option }, function (data) {
          //  $("#msg2").html(data);
        });
    }
}


document.onkeyup = function (e) {
    if (e.keyCode === 65 || e.keyCode === 68 || e.keyCode === 87 || e.keyCode === 83)
        KeyOptions("5", 5); //Stop     
};