(() => {
    window.addEventListener('load', function () {
        var viewBtns = document.querySelectorAll("#viewBtn");
        var types = document.querySelectorAll('.type');
        for (var i = 0; i < viewBtns.length; i++) {
            if (types[i].innerHTML !== 'Gold') {
                viewBtns[i].style.backgroundColor = 'rgb(' + [176, 176, 176].join(',') + ')';
            }
        }
    });
})();