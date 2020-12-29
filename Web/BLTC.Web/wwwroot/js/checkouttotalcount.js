(() => {
    window.onload = function () {
        var totalCount = document.getElementById('totalcount');
        var counts = document.querySelectorAll('#count');

        var total = 0;
        for (var i = 0; i < counts.length; i++) {
            total += parseInt(counts[i].innerText);
        }
        totalCount.innerText = total;
    };
})();