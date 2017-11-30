document.onkeydown = function (e) {
    switch (e.keyCode) {
        case 37:
            location.href = '/Robot/RobotArrows?id=' + 3;//left
            break;
        case 38:
            location.href = '/Robot/RobotArrows?id=' + 1;//up
            break;
        case 39:
            location.href = '/Robot/RobotArrows?id=' + 4;//right
            break;
        case 40:
            location.href = '/Robot/RobotArrows?id=' + 2;//down
            break;
    }
};

document.onkeyup = function (e) {
    switch (e.keyCode) {
        case 37:
            location.href = '/Robot/RobotArrows?id=' + 5;//left
            break;
        case 38:
            location.href = '/Robot/RobotArrows?id=' + 5;//up
            break;
        case 39:
            location.href = '/Robot/RobotArrows?id=' + 5;//right
            break;
        case 40:
            location.href = '/Robot/RobotArrows?id=' + 5;//down
            break;
    }
};