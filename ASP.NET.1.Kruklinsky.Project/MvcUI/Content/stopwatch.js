var CStopwatch = function () {
    var startAt = 0;
    var timeLeft = 0;

    this.start = function (duratation) {
        timeLeft = duratation;
        startAt = now();
    };

    this.time = function () {
        return timeLeft - (startAt ? now() - startAt : 72000);
    };

    var now = function () {
        return (new Date()).getTime();
    };
};

function pad(num, size) {
    var s = "0000" + num;
    return s.substr(s.length - size);
}
function formatTime(time) {
    var h = m = s = ms = 0;
    var newTime = '';

    h = Math.floor(time / (60 * 60 * 1000));
    time = time % (60 * 60 * 1000);
    m = Math.floor(time / (60 * 1000));
    time = time % (60 * 1000);
    s = Math.floor(time / 1000);
    ms = time % 1000;

    newTime = pad(h, 2) + ':' + pad(m, 2) + ':' + pad(s, 2);
    return newTime;
}   

var x = new CStopwatch();
var $time;
var clocktimer;
var finishUrl;

function stopwatch(id, url, duratation) {
    $time = document.getElementById(id);
    finishUrl = url;
    start(duratation);
}
function start(duratation) {
    clearInterval(clocktimer);
    clocktimer = setInterval("update()", 1);
    x.start(duratation);
}
function update() {
    if (x.time() <= 0) {
        clearInterval(clocktimer);
        $time.style.display = "none";
        finis();
    }
    $time.innerHTML = formatTime(x.time());
}
function finis() {
    window.alert('Time is out.')
    window.location.href = finishUrl;
}
