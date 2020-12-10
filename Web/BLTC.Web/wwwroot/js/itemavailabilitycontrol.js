(() => {
    window.onload = function () {
        var cards = document.querySelectorAll('.card-body');
        for (let i = 0; i < cards.length; i++) {
            var available = cards[i].querySelector('#available');

            var quantity = parseInt(available.innerText);

            if (quantity > 0) {
                available.innerText = 'In Stock';
            }
            else {
                available.innerText = 'Out of Stock'
                available.classList.value = "badge badge-danger";
            }
        }
    }
})();