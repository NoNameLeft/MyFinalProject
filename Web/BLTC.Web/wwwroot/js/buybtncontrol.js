(() => {
    window.onload = function () {
        document.getElementById('outOfStockAlert').style.display = 'none';
        var quanity = document.getElementById('available');

        if (quanity.value <= 0) {
            var buyBtn = document.getElementById('buyBtn');
            buyBtn.addEventListener('click', function (e) {
                e.preventDefault();
                document.getElementById('outOfStockAlert').style.display = 'block';
                document.getElementById('outOfStockAlert').innerText = "Currently this item is out of stock!"
                setTimeout(function () {
                    document.getElementById('outOfStockAlert').style.display = 'none';
                }, 4500);
            });
        }
    }
})();