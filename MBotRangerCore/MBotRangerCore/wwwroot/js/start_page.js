///Start page Author details
/*
var author1 = document.getElementById("author1");
var author2 = document.getElementById("author2");
var author3 = document.getElementById("author3");
var author4 = document.getElementById("author4");
var authorDetail1 = document.getElementById("detail1");
var authorDetail2 = document.getElementById("detail2");
var authorDetail3 = document.getElementById("detail3");
var authorDetail4 = document.getElementById("detail4");
authorDetail1.style.display = "none";
authorDetail2.style.display = "none";
authorDetail3.style.display = "none";
authorDetail4.style.display = "none";

author1.onclick = function () {
    ShowHideAuthorInfo(authorDetail1);
};

author2.onclick = function () {
    ShowHideAuthorInfo(authorDetail2);
};

author3.onclick = function () {
    ShowHideAuthorInfo(authorDetail3);
};

author4.onclick = function () {
    ShowHideAuthorInfo(authorDetail4);
};

function ShowHideAuthorInfo(infoId) {
    if (infoId.style.display === "none")
        infoId.style.display = "block";
    else
        infoId.style.display = "none";
}


*/

/*
///Hearbeat
var start = Date.now();
var hBeat = document.getElementById("heartBeat");
var diff = document.getElementById("diff");
hBeat.innerHTML = start;
var intial;
setInterval(myFunction, 5000);

function myFunction() {
    intial = hBeat.innerHTML;
    hBeat.innerHTML = Date.now();
    CheckDifference(intial, hBeat.innerHTML);
}

function CheckDifference(i, h) {
    var elapsed = (h - i) / 1000;

    if (elapsed > 7)
        diff.innerHTML = "closed";
    diff.innerHTML = elapsed;
}



window.onbeforeunload = function () { alert('beforeclosing'); };
window.onunload = function () { alert('closing'); };
*/